using System;
using Nidavellir.Audio;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private Resource m_playerHealthResource;
        [SerializeField] private SfxData loseHealthSfxData;


        private void Awake()
        {
            GameStateManager.Instance.OnValueReset += Reset;
        }

        private void Reset()
        {
            GameStateManager.Instance.OnValueReset -= Reset;
            
            m_playerHealthResource.ResourceController.ResetToStartValues();
        }

        public void TakeDamage(int amount)
        {
            if (GameStateManager.Instance.CurrentState == GameStateManager.State.Started)
            {
                this.m_playerHealthResource.ResourceController.SubtractResource(amount);
                SfxPlayer.Instance.PlayOneShot(loseHealthSfxData);

                if (this.m_playerHealthResource.ResourceController.CurrentValue <= 0f)
                    GameStateManager.Instance.TriggerGameOver();
            }
        }
    }
}