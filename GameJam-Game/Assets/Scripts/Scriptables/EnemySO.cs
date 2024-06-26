using Nidavellir.Towers.Projectiles;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create", order = 0)]
    public class EnemySO : ScriptableObject
    {
        [Header("Fields")] 
        [SerializeField] private new string name;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int currencyReward;
        [SerializeField] private float m_minimumSpeed;
        

        public string Name => name;
        public float MoveSpeed => moveSpeed;
        public int CurrencyReward => currencyReward;
        public float MinimumSpeed => this.m_minimumSpeed;
    }
}
