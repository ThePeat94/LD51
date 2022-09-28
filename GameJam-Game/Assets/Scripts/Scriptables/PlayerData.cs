using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private float m_rotationSpeed;

        public float MovementSpeed => this.m_movementSpeed;
        public float RotationSpeed => this.m_rotationSpeed;
    }
}