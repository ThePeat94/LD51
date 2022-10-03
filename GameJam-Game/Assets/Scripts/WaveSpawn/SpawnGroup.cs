using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Util
{
    [System.Serializable]
    public class SpawnGroup
    {
        [SerializeField] private GameObject m_enemyPrefab;
        [SerializeField] private int m_count;
        [SerializeField] private ResourceData m_healthData;
        [SerializeField] private EnemySO m_enemyData;
        
        public GameObject EnemyPrefab => this.m_enemyPrefab;
        public int Count => this.m_count;
        public ResourceData HealthData => this.m_healthData;
        public EnemySO EnemyData => this.m_enemyData;
    }
}