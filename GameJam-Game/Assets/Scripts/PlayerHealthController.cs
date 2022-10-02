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

        private GameStateManager m_gameStateManager;

        private void Awake()
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
        }

        public void TakeDamage(int amount)
        {
            if (this.m_gameStateManager.CurrentState == GameStateManager.State.Started)
            {
                this.m_playerHealthResource.ResourceController.SubtractResource(amount);
                SfxPlayer.Instance.PlayOneShot(loseHealthSfxData);

                if (this.m_playerHealthResource.ResourceController.CurrentValue <= 0f)
                    this.m_gameStateManager.TriggerGameOver();
            }
        }
    }
}