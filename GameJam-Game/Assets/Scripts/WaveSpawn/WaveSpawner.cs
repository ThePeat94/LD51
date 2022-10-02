using System;
using System.Collections.Generic;
using System.Linq;
using Nidavellir.EventArgs;
using Nidavellir.PathManagement;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Nidavellir.Util
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveData> m_waveData;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private Path m_path;
        [SerializeField] private Vector3 m_enemyOffset;
        [SerializeField] private TextMeshProUGUI spawnCountdownText;

        private GameStateManager m_gameStateManager;
        private List<GameObject> m_spawnedEnemies = new();
        private Queue<WaveData> m_waves;

        private void Awake()
        {
            if(this.m_path == null)
                this.m_path = FindObjectOfType<Path>();

            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            this.m_waves = new(this.m_waveData);
        }

        private void Start()
        {
            TimerSystem.Instance.OnTimerEndTick += this.OnTimerHasEndedTick;
            TimerSystem.Instance.OnTotalTimeTick += OnTotalTimeTick;
        }

        private void OnTotalTimeTick(float totalTime)
        {
            var remainingTimeForNextSpawn = Mathf.RoundToInt((TimerSystem.TickerTime - (TimeSpan.FromSeconds(totalTime).Seconds % TimerSystem.TickerTime)));
            spawnCountdownText.text = $"{remainingTimeForNextSpawn}";
        }

        private void OnTimerHasEndedTick()
        {
            if (this.m_gameStateManager.CurrentState == GameStateManager.State.Started)
                this.SpawnWave();
        }

        private void SpawnWave()
        {
            if (this.m_waves.Count == 0)
                return;
            
            var currentWave = this.m_waves.Dequeue();

            var currentSpawnPosition = this.m_spawnPoint.position;
            
            foreach (var spawnGroup in currentWave.SpawnGroups)
            {
                for (var i = 0; i < spawnGroup.Count; i++)
                {
                    var enemy = GameObject.Instantiate(spawnGroup.EnemyPrefab, currentSpawnPosition, Quaternion.identity);
                    var pathWalker = enemy.GetComponent<EnemyPathWalker>();
                    pathWalker.Path = this.m_path;
                    pathWalker.ReachedGoal += EnemyReachedGoal;
                    currentSpawnPosition += this.m_enemyOffset;
                    this.m_spawnedEnemies.Add(enemy);
                    enemy.GetComponent<EnemyHealthController>().ResourceController.ValueChanged += ((sender, args) => this.EnemyHealthChanged(args, enemy));
                }
            }
        }

        private void EnemyReachedGoal(object sender, System.EventArgs e)
        {
            this.RemoveEnemy(sender as GameObject);
        }

        private void EnemyHealthChanged(ResourceValueChangedEventArgs e, GameObject enemy)
        {
            if (e.NewValue <= 0)
            {
                this.RemoveEnemy(enemy);
            }
        }

        private void RemoveEnemy(GameObject enemy)
        {
            if(this.m_spawnedEnemies.Contains(enemy))
            {
                this.m_spawnedEnemies.Remove(enemy);

                if (this.m_spawnedEnemies.Count == 0 && this.m_waves.Count == 0 && this.m_gameStateManager.CurrentState != GameStateManager.State.GameOver)
                {
                    this.m_gameStateManager.TriggerGameWon();
                }
            }
        }

        private void OnDestroy()
        {
            TimerSystem.Instance.OnTimerEndTick -= this.OnTimerHasEndedTick;
        }
    }
}