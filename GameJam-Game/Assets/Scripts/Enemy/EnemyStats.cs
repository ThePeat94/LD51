#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private EnemySO m_initialStats;
        [SerializeField] private ParticleSystem buffParticleSystem;
        private float m_movementSpeed;
        private int m_level;
        private int m_damage;

        private EnemyHealthController m_healthController;
        private EnemyBuffData m_currentDebuff;
        private int m_debuffFrameCount;

        public float MovementSpeed => this.m_movementSpeed;
        public int Level => this.m_level;
        public int Damage => this.m_damage;

        private void Awake()
        {
            this.m_healthController = this.GetComponent<EnemyHealthController>();
            this.m_movementSpeed = this.m_initialStats.MoveSpeed;
            this.m_level = 1;
            this.m_damage = 1;
        }

        private void FixedUpdate()
        {
            if (this.m_debuffFrameCount > 0)
            {
                this.m_debuffFrameCount--;
            }

            if (this.m_debuffFrameCount == 0 && this.m_currentDebuff != null)
            {
                this.m_movementSpeed += this.m_currentDebuff.MovementSpeedIncrease;
                this.m_currentDebuff = null;
            }
        }

        public void ApplyBuff(EnemyBuffData buffData)
        {
            this.m_movementSpeed += buffData.MovementSpeedIncrease;
            this.m_healthController.ResourceController.Add(buffData.HealthIncrease);
            this.m_level++;
            this.m_damage++;
            buffParticleSystem.Play();
        }

        public void ApplyDebuff(EnemyBuffData debuffData)
        {

            if (this.m_currentDebuff == null)
            {
                this.m_movementSpeed -= debuffData.MovementSpeedIncrease;
                this.m_currentDebuff = debuffData;
            }
            else if (this.m_currentDebuff.MovementSpeedIncrease < debuffData.MovementSpeedIncrease)
            {
                var delta = debuffData.MovementSpeedIncrease - this.m_currentDebuff.MovementSpeedIncrease;
                this.m_movementSpeed -= delta;
                this.m_currentDebuff = debuffData;
            }

            this.m_debuffFrameCount = 30;
        }
    }
}
