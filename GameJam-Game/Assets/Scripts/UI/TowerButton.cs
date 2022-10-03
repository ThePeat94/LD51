using System;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Resource m_currencyResource;
        
        
        public Action<TowerSO> OnButtonClick;

        private TowerSO towerSo;

        public void SetTowerSo(TowerSO towerSo)
        {
            this.towerSo = towerSo;

            image.sprite = towerSo.Icon;

            priceText.text = towerSo.Price.ToString();
        }

        private void Awake()
        {
            this.m_currencyResource.ResourceController.ValueChanged += OnCurrencyChanged;
            button.onClick.AddListener(() =>
            {
                OnButtonClick?.Invoke(towerSo);
            });
        }

        private void OnDestroy()
        {
            this.m_currencyResource.ResourceController.ValueChanged -= this.OnCurrencyChanged;
        }

        private void OnCurrencyChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.button.interactable = e.NewValue >= this.towerSo.Price;
            this.priceText.color = this.button.interactable ? Color.white : Color.red;
        }
    }
}