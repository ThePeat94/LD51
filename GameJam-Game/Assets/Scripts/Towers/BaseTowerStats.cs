using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Towers
{
    public class BaseTowerStats : MonoBehaviour
    {
        [SerializeField] private TowerSO m_towerConfig;

        private float m_damage;
        private float m_range;
        private float m_attackSpeed;

        public float Damage => this.m_damage;
        public float Range => this.m_range;
        public float AttackSpeed => this.m_attackSpeed;

        public void Awake()
        {
            this.m_damage = this.m_towerConfig.Damage;
            this.m_range = this.m_towerConfig.TowerRange;
            this.m_attackSpeed = this.m_towerConfig.AttackSpeed;
        }

        public void ApplyUpgrade(TowerUpgradeSO upgradeData)
        {
            this.m_damage += upgradeData.DamageIncrease;
            this.m_range += upgradeData.TowerRangeIncrease;
            this.m_attackSpeed += upgradeData.AttackSpeedIncrease;
        }   
    }
}