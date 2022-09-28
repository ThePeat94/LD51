using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Resource Data", menuName = "Resource Data", order = 0)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private float m_startingValue;
        [SerializeField] private float m_maxValue;

        public float StartingValue => this.m_startingValue;
        public float MaxValue => this.m_maxValue;
    }
}