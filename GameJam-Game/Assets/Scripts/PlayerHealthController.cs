using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private Resource m_playerHealthResource;

        private GameStateManager m_gameStateManager;

        private void Awake()
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
        }

        public void TakeDamage(int amount)
        {
            if (this.m_gameStateManager.CurrentState == GameStateManager.State.Started)
            {
                this.m_playerHealthResource.ResourceController.UseResource(amount);

                if (this.m_playerHealthResource.ResourceController.CurrentValue <= 0f)
                    this.m_gameStateManager.TriggerGameOver();
            }
        }
    }
}