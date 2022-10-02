using System;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private EnemySO m_enemyConfig;
        [SerializeField] private ResourceData m_resourceData;
        private ResourceController m_resourceController;
        private bool dead;
        
        public ResourceController ResourceController => this.m_resourceController;

        private void Awake()
        {
            this.m_resourceController = new(this.m_resourceData);
        }

        public void TakeDamage(float amount)
        {
            this.m_resourceController.SubtractResource(amount);
            
            if (this.m_resourceController.CurrentValue <= 0 && !dead)
            {
                dead = true;
                CurrencyController.Instance.AddCurrency(this.m_enemyConfig.CurrencyReward);
                Destroy(this.gameObject);
            }
        }
    }
}