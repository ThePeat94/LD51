using System.Collections.Generic;
using System.Linq;
using Nidavellir.Trigger;
using Nidavellir.Turrets.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nidavellir.Turrets
{
    public  class Tower : MonoBehaviour
    {
        [Header("References")] 
        public TowerSO TowerSettings;
        [FormerlySerializedAs("AttackTrigger")] public SphereBaseTrigger attackBaseTrigger;

        private float timeUntilNextAttack;
        private GameObject currentTarget;
        private List<GameObject> enemiesInRange = new List<GameObject>();
        private bool isPlaced = true; //TODO change once the tower is placeable

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            attackBaseTrigger.Init(Vector3.one * TowerSettings.TowerRange);
            timeUntilNextAttack = 0;

            attackBaseTrigger.EventOnTriggerEnter += AddEnemyInRange;
            attackBaseTrigger.EventOnTriggerExit += RemoveEnemyInRange;
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
                var projectile = Instantiate(TowerSettings.Projectile.gameObject).GetComponent<Projectile>();
                projectile.transform.position = transform.position; //TODO add projectile spawnpoint
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
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            attackBaseTrigger?.SetSize(Vector3.one * TowerSettings.TowerRange);
        }
#endif
    }
}
