using System;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyBuffer : MonoBehaviour
    {
        [SerializeField] private EnemyBuffData m_enemyBuffData;

        private void Start()
        {
            TimerSystem.Instance.OnTimerEndTick += this.OnTimerTickEnd;
        }

        private void OnTimerTickEnd()
        {
            Debug.Log("ENEMIES BUFFED");
            foreach (var enemyStat in FindObjectsOfType<EnemyStats>())
            {
                enemyStat.ApplyBuff(this.m_enemyBuffData);
            }
        }
    }
}