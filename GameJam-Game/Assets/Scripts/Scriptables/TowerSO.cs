using System.Collections.Generic;
using Nidavellir.Towers;
using Nidavellir.Towers.Projectiles;
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
        [SerializeField] private float damage;
        [SerializeField] private int m_shootFrameCoolDown;


        [Header("Shop Values")] 
        public Sprite Icon;
        [SerializeField] private int price;

        [Header("References")]
        public Tower TowerPrefab;
        public Projectile Projectile;
        public List<TowerUpgradeSO> PossibleUpgrades;

        public string Name => name;
        public float TowerRange => towerRange;
        public float AttackSpeed => attackSpeed;
        public float Damage => damage;

        public int Price => price;
        public int MaxLevel => PossibleUpgrades.Count + 1;
        public int ShootFrameCoolDown => this.m_shootFrameCoolDown;

    }
}