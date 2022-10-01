using UnityEngine;

namespace Nidavellir.Util
{
    [System.Serializable]
    public class SpawnGroup
    {
        [SerializeField] private GameObject m_enemyPrefab;
        [SerializeField] private int m_count;

        public GameObject EnemyPrefab => this.m_enemyPrefab;
        public int Count => this.m_count;
    }
}