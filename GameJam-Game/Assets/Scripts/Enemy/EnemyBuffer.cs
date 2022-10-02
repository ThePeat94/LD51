using System;
using Nidavellir.Audio;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyBuffer : MonoBehaviour
    {
        [SerializeField] private EnemyBuffData m_enemyBuffData;
        [SerializeField] private SfxData buffEnemySfxData;

        private void Start()
        {
            TimerSystem.Instance.OnTimerEndTick += this.OnTimerTickEnd;
        }

        private void OnTimerTickEnd()
        {
            Debug.Log("ENEMIES BUFFED");
            
            SfxPlayer.Instance.PlayOneShot(buffEnemySfxData);
            
            foreach (var enemyStat in FindObjectsOfType<EnemyStats>())
            {
                enemyStat.ApplyBuff(this.m_enemyBuffData);
            }
        }
    }
}