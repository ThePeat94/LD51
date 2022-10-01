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
        private bool isPlaced = false; //TODO change once the tower is placeable

        //TODO this is currently used for testing
        private void Start()
        {
            Place(transform.position);
            Init();
        }

        public void Init()
        {
            AttackBaseTrigger.Init(Vector3.one * TowerSettings.TowerRange);
            timeUntilNextAttack = 0;

            AttackBaseTrigger.EventOnTriggerEnter += AddEnemyInRange;
            AttackBaseTrigger.EventOnTriggerExit += RemoveEnemyInRange;
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
                projectile.Init(closestEnemy, closestEnemy.transform.position);

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
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            AttackBaseTrigger?.SetSize(Vector3.one * TowerSettings.TowerRange);
        }
#endif
    }
}
