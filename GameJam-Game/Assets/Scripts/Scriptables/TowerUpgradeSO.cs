using Nidavellir.Towers.Projectiles;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "TowerUpgrade", menuName = "Tower Upgrade/Create", order = 0)]
    public class TowerUpgradeSO : ScriptableObject
    {
        [Header("Fields")]
        [SerializeField] private float towerRangeIncrease;
        [SerializeField] private float attackSpeedIncrease;
        [SerializeField] private float damageIncrease;

        [Header("Fields")] 
        [SerializeField] private int price;
        
        [Header("References")]
        public Projectile Projectile;

        public float TowerRangeIncrease => towerRangeIncrease;
        public float AttackSpeedIncrease => attackSpeedIncrease;
        public float DamageIncrease => damageIncrease;

        public int Price => price;
    }
}
