using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class EnemyBuffData : ScriptableObject
    {
        [SerializeField] private float m_movementSpeedIncrease;
        [SerializeField] private float m_healthIncrease;

        public float MovementSpeedIncrease => this.m_movementSpeedIncrease;
        public float HealthIncrease => this.m_healthIncrease;
    }
}