using System.Collections.Generic;
using System.Linq;
using Nidavellir.Scriptables;
using Nidavellir.Tower.Projectiles;
using Nidavellir.Trigger;
using UnityEngine;

namespace Nidavellir.Tower
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
        
        //TODO this is currently used for testing
        private void Start()
        {
            Place(transform.position);
            Init();
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

            if (timeUntilNextAttack <= 0 && enemiesInRange.Count > 0)
            {
                var closestEnemy = GetClosestEnemy();
                var projectile = Object.Instantiate(TowerSettings.Projectile.gameObject).GetComponent<Projectile>();
                projectile.transform.position = ProjectileSpawnPoint.transform.position;
                projectile.Init(closestEnemy, closestEnemy.transform.position, TowerSettings.Damage);

                timeUntilNextAttack = TowerSettings.AttackSpeed;
            }
        }
        
        private GameObject GetClosestEnemy()
        {
            return enemiesInRange.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
        }

        private void AddEnemyInRange(GameObject enemy)
        {
            enemiesInRange.Add(enemy);
        }
        
        private void RemoveEnemyInRange(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        public void Place(Vector3 position)
        {
            transform.position = position;
            isPlaced = true;
        }

        public void Unplace()
        {
            isPlaced = false;
        }

        [ContextMenu("Upgrade")]
        public void Upgrade()
        {
            if (CurrentLevel < TowerSettings.MaxLevel - 1)
            {
                ApplyUpgrade(TowerSettings.PossibleUpgrades[CurrentLevel]);
            }
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
