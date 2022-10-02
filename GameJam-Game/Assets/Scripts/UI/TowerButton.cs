using System;
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
            button.onClick.AddListener(() =>
            {
                OnButtonClick?.Invoke(towerSo);
            });
        }
    }
}