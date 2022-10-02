using System;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using Nidavellir.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class TowerUI : MonoBehaviour
    {
        [SerializeField] private Resource m_currencyResource;
        
        [SerializeField] private TextMeshProUGUI m_nameLevelText;
        [SerializeField] private TextMeshProUGUI m_damageText;
        [SerializeField] private TextMeshProUGUI m_attackSpeedText;
        [SerializeField] private TextMeshProUGUI m_rangeText;
        [SerializeField] private TextMeshProUGUI m_costsText;
        [SerializeField] private Button m_upgradeButton;
        
        private Tower m_towerToDisplay;

        private void Awake()
        {
            this.m_upgradeButton.onClick.AddListener(this.UpgradeTower);
        }

        public void DisplayTower(Tower toDisplay)
        {
            this.gameObject.SetActive(true);
            this.m_towerToDisplay = toDisplay;
            this.UpdateUI();
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            this.m_towerToDisplay = null;
            this.m_currencyResource.ResourceController.ValueChanged -= this.OnCurrencyChanged;
        }

        private void UpgradeTower()
        {
            this.m_towerToDisplay.Upgrade();
            this.UpdateUI();
        }

        private void OnCurrencyChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_upgradeButton.interactable = this.m_currencyResource.ResourceController.CanAfford(this.m_towerToDisplay.CostsForNextLevel);
        }
        
        
        private void UpdateUI()
        {
            this.m_nameLevelText.text = $"{this.m_towerToDisplay.TowerSettings.Name} - Lvl. {this.m_towerToDisplay.CurrentLevel}";
            this.m_damageText.text = $"{this.m_towerToDisplay.Damage:F2}";
            this.m_attackSpeedText.text = $"{this.m_towerToDisplay.AttackSpeed:F2}";
            this.m_costsText.text = $"{this.m_towerToDisplay.CostsForNextLevel}";
            this.m_rangeText.text = $"{this.m_towerToDisplay.TowerRange:F2}";
            this.m_upgradeButton.interactable = this.m_towerToDisplay.CurrentLevel < this.m_towerToDisplay.TowerSettings.PossibleUpgrades.Count - 1 && this.m_currencyResource.ResourceController.CanAfford(this.m_towerToDisplay.CostsForNextLevel);
        }
    }
}