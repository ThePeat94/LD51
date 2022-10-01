using Nidavellir.Tower.Projectiles;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Tower/Create", order = 0)]
    public class TowerSO : ScriptableObject
    {
        [Header("Fields")] 
        [SerializeField] private new string name;
        [SerializeField] private float towerRange;
        [SerializeField] private float attackSpeed;

        [Header("Shop Values")] 
        public Sprite Icon;
        [SerializeField] private int price;

        [Header("References")]
        public Projectile Projectile;

        public string Name => name;
        public float TowerRange => towerRange;
        public float AttackSpeed => attackSpeed;
        public float Damage => Projectile.Damage;

        public int Price => price;
    }
}