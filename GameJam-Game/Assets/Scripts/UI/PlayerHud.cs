using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using Nidavellir.Towers;
using UnityEngine;

namespace Nidavellir.UI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private GameObject m_loseScreenPanel;
        [SerializeField] private GameObject m_winScreenPanel;
        [SerializeField] private GameObject m_mainGamePanel;
        [SerializeField] private PauseMenu m_pauseMenu;
        [SerializeField] private TowerUI m_towerUI;
        [SerializeField] private SfxData winMusic;
        [SerializeField] private SfxData loseMusic;


        public void ShowLoseScreen()
        {
            this.m_mainGamePanel.SetActive(false);
            this.m_loseScreenPanel.SetActive(true);
            
            SfxPlayer.Instance.PlayOneShot(loseMusic);
        }

        public void ShowPauseMenu()
        {
            this.m_pauseMenu.ShowMenu();
            this.m_mainGamePanel.SetActive(false);
        }

        public void HidePauseMenu()
        {
            GameStateManager.Instance.TriggerUnpause();
            this.m_pauseMenu.CloseMenu();
            this.m_mainGamePanel.SetActive(true);
        }

        public void ShowTower(Tower tower)
        {
            this.m_towerUI.DisplayTower(tower);
        }

        public void ShowWonScreen()
        {
            this.m_mainGamePanel.SetActive(false);
            this.m_winScreenPanel.SetActive(true);
            
            SfxPlayer.Instance.PlayOneShot(winMusic);
        }
    }
}