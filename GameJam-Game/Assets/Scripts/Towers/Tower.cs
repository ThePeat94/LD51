using System.Collections.Generic;
using System.Linq;
using Nidavellir.Scriptables;
using Nidavellir.Towers.Projectiles;
using Nidavellir.Trigger;
using UnityEngine;

namespace Nidavellir.Towers
{
    public class Tower : MonoBehaviour
    {
        [Header("References")] 
        public TowerSO TowerSettings;
        public SphereBaseTrigger AttackBaseTrigger;
        public Transform ProjectileSpawnPoint;
        
        private float timeUntilNextAttack;
        private GameObject currentTarget;
        private List<GameObject> enemiesInRange = new List<GameObject>();
        private bool isPlaced;
        private List<TowerUpgradeSO> appliedUpgrades = new List<TowerUpgradeSO>();
        
        public float TowerRange { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public float Damage { get; protected set; }
        public Projectile Projectile { get; protected set; }
        public int CurrentLevel => appliedUpgrades.Count;
        public float CostsForNextLevel => this.TowerSettings.PossibleUpgrades[this.CurrentLevel].Price;
        public bool IsPlaced => this.isPlaced;


        [ContextMenu("Place")]
        private void DebugPlace()
        {
            Init();
            Place();
        }
        
        public void Init()
        {
            TowerRange = TowerSettings.TowerRange;
            AttackSpeed = TowerSettings.AttackSpeed;
            Damage = TowerSettings.Damage;
            Projectile = TowerSettings.Projectile;
            
            AttackBaseTrigger.Init(Vector3.one * TowerSettings.TowerRange);
            AttackBaseTrigger.EventOnTriggerEnter += AddEnemyInRange;
            AttackBaseTrigger.EventOnTriggerExit += RemoveEnemyInRange;
            
            timeUntilNextAttack = 0;
        }

        private void Update()
        {
            if(!isPlaced)
                return;
            
            Shoot(Time.deltaTime);
        }

        private void Shoot(float deltaTime)
        {
            timeUntilNextAttack -= deltaTime;

            if (timeUntilNextAttack <= 0)
            {
                var closestEnemy = GetClosestEnemy();

                if (closestEnemy == null)
                    return;
                
                var projectile = Object.Instantiate(Projectile.gameObject).GetComponent<Projectile>();
                projectile.transform.position = ProjectileSpawnPoint.transform.position;
                projectile.Init(closestEnemy, closestEnemy.transform.position, Damage);

                timeUntilNextAttack = AttackSpeed;
            }
        }
        
        private GameObject GetClosestEnemy()
        {
            var enemiesHitByOverlapSphere = Physics.OverlapSphere(this.transform.position, this.TowerRange)
                .Where(c => c.TryGetComponent<EnemyHealthController>(out _)).ToArray();
            
            if(enemiesHitByOverlapSphere.Length > 0)
                return enemiesHitByOverlapSphere.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First().gameObject;

            return null;
        }

        private void AddEnemyInRange(GameObject enemy)
        {
            enemiesInRange.Add(enemy);
        }
        
        private void RemoveEnemyInRange(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        public bool Place()
        {
            if (CurrencyController.Instance.BuyItem(TowerSettings.Price))
            {
                isPlaced = true;
                return true;
            }
            
            return false;
        }

        public void Unplace()
        {
            isPlaced = false;
        }

        [ContextMenu("Upgrade")]
        public bool Upgrade()
        {
            if (CurrentLevel < TowerSettings.MaxLevel - 1)
            {
                var nextUpgrade = TowerSettings.PossibleUpgrades[CurrentLevel];

                if (CurrencyController.Instance.BuyItem(nextUpgrade.Price))
                {
                    ApplyUpgrade(nextUpgrade);
                    return true;
                }
            }

            return false;
        }

        protected virtual void ApplyUpgrade(TowerUpgradeSO towerUpgradeSo)
        {
            appliedUpgrades.Add(towerUpgradeSo);

            TowerRange += towerUpgradeSo.TowerRangeIncrease;
            AttackSpeed -= towerUpgradeSo.AttackSpeedIncrease;
            Damage += towerUpgradeSo.DamageIncrease;

            if (towerUpgradeSo.Projectile != null)
                Projectile = towerUpgradeSo.Projectile;
        }
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            AttackBaseTrigger?.SetSize(Vector3.one * TowerSettings.TowerRange);
        }
#endif
    }
}
