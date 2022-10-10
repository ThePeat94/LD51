using System;
using Nidavellir.Scriptables;
using Nidavellir.Towers.Projectiles;
using Nidavellir.Towers.TargetFinding;
using UnityEngine;

namespace Nidavellir.Towers.Shooting
{
    public class SingleProjectileShooter : MonoBehaviour, ITowerShootStrategy
    {
        [SerializeField] private TowerSO m_towerConfig;
        [SerializeField] private Transform m_projectileSpawnPoint;

        private BaseTowerStats m_baseTowerStats;
        private BaseTowerAnimator m_baseTowerAnimator;
        private ITowerTargetStrategy m_targetStrategy;
        private int m_currentShootCoolDown;

        public GameObject Target { get; private set; }

        private void Awake()
        {
            this.m_baseTowerStats = this.GetComponent<BaseTowerStats>();
            this.m_baseTowerAnimator = this.GetComponent<BaseTowerAnimator>();
            this.m_targetStrategy = new ClosestEnemyStrategy(this.m_baseTowerStats, this.transform);

            if (this.m_projectileSpawnPoint == null)
            {
                Debug.LogError($"{this.gameObject.name}: no ProjectileSpawnPoint defined");
                this.m_projectileSpawnPoint = this.transform;
            }
        }

        public void Shoot()
        {
            var projectile = Instantiate(this.m_towerConfig.Projectile.gameObject)
                .GetComponent<Projectile>();
            projectile.transform.position = this.m_projectileSpawnPoint.position;
            projectile.Init(this.Target.gameObject, this.Target.transform.position, this.m_baseTowerStats.Damage);
            this.m_currentShootCoolDown = this.m_towerConfig.ShootFrameCoolDown;
        }

        private void Update()
        {
            var enemies = this.m_targetStrategy.FindEnemiesInRange(1);
            if (enemies.Count > 0)
            {
                this.Target = enemies[0]
                    .gameObject;
                this.m_baseTowerAnimator.Animate(this.Target);
            }
        }

        private void FixedUpdate()
        {
            if (this.m_currentShootCoolDown > 0)
            {
                this.m_currentShootCoolDown--;
            }
            else if (this.Target != null)
            {
                this.Shoot();
            }
        }
    }
}