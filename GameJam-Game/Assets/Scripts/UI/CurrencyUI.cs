using System;
using DG.Tweening;
using Nidavellir.EventArgs;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class CurrencyUI : MonoBehaviour
    {
        [Header("References")] 
        public TextMeshProUGUI ValueText;

        private float currentCurrencyValue;
        // private Tweener m_coinLossTweener;

        private void Start()
        {
            UpdateCurrencyValueText((int) CurrencyController.Instance.CurrencyResource.ResourceController.CurrentValue);
            CurrencyController.Instance.CurrencyResource.ResourceController.ValueChanged += CurrencyValueChanged;
            currentCurrencyValue = CurrencyController.Instance.CurrencyResource.ResourceController.CurrentValue;
        }

        private void CurrencyValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            UpdateCurrencyValueText((int)e.NewValue);
            if (currentCurrencyValue < e.NewValue) AnimateCoinGain();
            if (currentCurrencyValue > e.NewValue) AnimateCoinLoss();
            }

        private void UpdateCurrencyValueText(int newValue)
        {
            ValueText.text = newValue.ToString();
        }

        private void AnimateCoinGain()
        {
            this.ValueText.transform.DORewind();
            this.ValueText.transform.DOScale(1.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
        
        private void AnimateCoinLoss()
        {
            this.ValueText.transform.DORewind();
            this.ValueText.transform.DOLocalMove(this.ValueText.transform.localPosition + new Vector3(0, -25, 0), 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
