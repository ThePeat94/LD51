using System;
using DG.Tweening;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using Nidavellir.Towers;
using UnityEngine;

namespace Nidavellir.UI
{
    public class TowerUpgradeHintUI : MonoBehaviour
    {
        [SerializeField] private Resource m_currencyResource;

        private Tower m_tower;

        private void Awake()
        {
            this.m_tower = this.GetComponentInParent<Tower>();
            this.m_currencyResource.ResourceController.ValueChanged += this.OnCurrencyChange;
            this.gameObject.SetActive(this.m_currencyResource.ResourceController.CanAfford(this.m_tower.CostsForNextLevel));
        }

        private void Start()
        {
            this.transform.DOLocalMove(this.transform.localPosition + (this.transform.up * 3), 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnDestroy()
        {
            this.m_currencyResource.ResourceController.ValueChanged -= this.OnCurrencyChange;
        }

        private void OnCurrencyChange(object sender, ResourceValueChangedEventArgs e)
        {
            this.gameObject.SetActive(this.m_tower.CostsForNextLevel <= e.NewValue);
        }
    }
}