using Nidavellir.Turrets.Projectiles;
using UnityEngine;

namespace Nidavellir.Turrets
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Tower/Create", order = 0)]
    public class TowerSO : ScriptableObject
    {
        [Header("Fields")] 
        [SerializeField] private new string name;
        [SerializeField] private float towerRange;
        [SerializeField] private float attackSpeed;
        
        [Header("References")]
        public Projectile Projectile;

        public string Name => name;
        public float TowerRange => towerRange;
        public float AttackSpeed => attackSpeed;
        public float Damage => Projectile.Damage;
    }
}