using Nidavellir.Towers;
using UnityEngine;

namespace Nidavellir.UI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private GameObject m_loseScreenPanel;
        [SerializeField] private GameObject m_mainGamePanel;
        [SerializeField] private PauseMenu m_pauseMenu;
        [SerializeField] private TowerUI m_towerUI;


        public void ShowLoseScreen()
        {
            this.m_mainGamePanel.SetActive(false);
            this.m_loseScreenPanel.SetActive(true);
        }

        public void ShowPauseMenu()
        {
            this.m_pauseMenu.ShowMenu();
            this.m_mainGamePanel.SetActive(false);
        }

        public void HidePauseMenu()
        {
            this.m_pauseMenu.CloseMenu();
            this.m_mainGamePanel.SetActive(true);
            FindObjectOfType<GameStateManager>().HidePauseMenu();
        }

        public void ShowTower(Tower tower)
        {
            this.m_towerUI.DisplayTower(tower);
        }
    }
}