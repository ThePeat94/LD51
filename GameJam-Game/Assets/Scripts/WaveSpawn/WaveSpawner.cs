using System;
using System.Collections.Generic;
using System.Linq;
using Nidavellir.PathManagement;
using Nidavellir.Scriptables;
using UnityEngine;
using Random = System.Random;

namespace Nidavellir.Util
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveData> m_waves;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private Path m_path;
        [SerializeField] private Vector3 m_enemyOffset;

        private GameStateManager m_gameStateManager;


        private int m_currentWaveIndex = 0;

        private void Awake()
        {
            if(this.m_path == null)
                this.m_path = FindObjectOfType<Path>();

            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
        }

        private void Start()
        {
            TimerSystem.Instance.OnTimerEndTick += this.OnTimerHasEndedTick;
        }

        private void OnTimerHasEndedTick()
        {
            if (this.m_gameStateManager.CurrentState == GameStateManager.State.Started)
                this.SpawnWave();
        }

        private void SpawnWave()
        {
            if (this.m_currentWaveIndex >= this.m_waves.Count)
                return;
            
            var currentWave = this.m_waves[this.m_currentWaveIndex];

            var currentSpawnPosition = this.m_spawnPoint.position;
            
            foreach (var spawnGroup in currentWave.SpawnGroups)
            {
                for (var i = 0; i < spawnGroup.Count; i++)
                {
                    var enemy = GameObject.Instantiate(spawnGroup.EnemyPrefab, currentSpawnPosition, Quaternion.identity)
                        .GetComponent<EnemyPathWalker>();
                    enemy.Path = this.m_path;
                    currentSpawnPosition += this.m_enemyOffset;
                }
            }

            this.m_currentWaveIndex++;
        }

        private void OnDestroy()
        {
            TimerSystem.Instance.OnTimerEndTick -= this.OnTimerHasEndedTick;
        }
    }
}