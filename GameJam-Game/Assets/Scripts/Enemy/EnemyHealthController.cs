using System;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_resourceData;
        private ResourceController m_resourceController;

        public ResourceController ResourceController => this.m_resourceController;

        private void Awake()
        {
            this.m_resourceController = new(this.m_resourceData);
        }

        public void TakeDamage(float amount)
        {
            this.m_resourceController.SubtractResource(amount);
            if (this.m_resourceController.CurrentValue <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}