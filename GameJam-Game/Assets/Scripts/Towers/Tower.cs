using System.Collections.Generic;
using System.Linq;
using Nidavellir.Scriptables;
using Nidavellir.Towers.Projectiles;
using Nidavellir.Trigger;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Towers
{
    public class Tower : MonoBehaviour
    {
        [Header("References")] 
        public TowerSO TowerSettings;
        public SphereBaseTrigger AttackBaseTrigger;
        public Transform ProjectileSpawnPoint;
        public Transform Muzzle;
        
        private float timeUntilNextAttack;
        private GameObject currentTarget;
        private List<GameObject> enemiesInRange = new List<GameObject>();
        private bool isPlaced;
        private List<TowerUpgradeSO> appliedUpgrades = new List<TowerUpgradeSO>();
        private GameObject closestEnemy = null;
        private Queue<TowerUpgradeSO> m_towerUpgrades;
        
        public float TowerRange { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public float Damage { get; protected set; }
        public Projectile Projectile { get; protected set; }
        public int CurrentLevel { get; private set; }
        public float CostsForNextLevel => this.m_towerUpgrades.Count > 0 ? this.m_towerUpgrades.Peek().Price : 0;
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

            closestEnemy = GetClosestEnemy();
            Shoot(Time.deltaTime);

            if (Muzzle != null && closestEnemy != null)
            {
                Muzzle.LookAt(new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.position.y, closestEnemy.transform.position.z));
            }
        }

        private void Shoot(float deltaTime)
        {
            timeUntilNextAttack -= deltaTime;

            if (timeUntilNextAttack <= 0)
            {
                if (closestEnemy == null)
                    return;
                
                var projectile = Object.Instantiate(Projectile.gameObject).GetComponent<Projectile>();

                if (ProjectileSpawnPoint != null)
                {
                    projectile.transform.position = ProjectileSpawnPoint.transform.position;
                }
                else
                {
                    Debug.LogError($"{ TowerSettings.Name }: no ProjectileSpawnPoint defined");
                    projectile.transform.position = transform.position;
                }
                
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
                this.CurrentLevel++;
                this.m_towerUpgrades = new(this.TowerSettings.PossibleUpgrades);
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
            if (this.m_towerUpgrades.Count > 0)
            {
                var nextUpgrade = this.m_towerUpgrades.Peek();

                if (CurrencyController.Instance.BuyItem(nextUpgrade.Price))
                {
                    ApplyUpgrade(nextUpgrade);
                    this.m_towerUpgrades.Dequeue();
                    return true;
                }
            }

            return false;
        }

        protected virtual void ApplyUpgrade(TowerUpgradeSO towerUpgradeSo)
        {
            TowerRange += towerUpgradeSo.TowerRangeIncrease;
            AttackSpeed -= towerUpgradeSo.AttackSpeedIncrease;
            Damage += towerUpgradeSo.DamageIncrease;

            if (towerUpgradeSo.Projectile != null)
                Projectile = towerUpgradeSo.Projectile;

            this.CurrentLevel++;
        }
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            AttackBaseTrigger?.SetSize(Vector3.one * TowerSettings.TowerRange);
        }
#endif
        public bool CanUpgrade() => this.HasUpgradeAvailable() & CurrencyController.Instance.CurrencyResource.ResourceController.CanAfford(this.CostsForNextLevel);
        public bool HasUpgradeAvailable() => this.m_towerUpgrades.Count > 0;
    }
}
