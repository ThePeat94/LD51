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
        private float m_movementSpeed;
        private int m_level;
        private int m_damage;

        private EnemyHealthController m_healthController;

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

        public void ApplyBuff(EnemyBuffData buffData)
        {
            this.m_movementSpeed += buffData.MovementSpeedIncrease;
            this.m_healthController.ResourceController.Add(buffData.HealthIncrease);
            this.m_level++;
            this.m_damage++;
        }
    }
}
