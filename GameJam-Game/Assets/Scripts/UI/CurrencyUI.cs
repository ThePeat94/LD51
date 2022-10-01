using System;
using Nidavellir.EventArgs;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class CurrencyUI : MonoBehaviour
    {
        [Header("References")] 
        public TextMeshProUGUI ValueText;
        
        private void Start()
        {
            UpdateCurrencyValueText((int) CurrencyController.Instance.CurrencyResource.ResourceController.CurrentValue);
            CurrencyController.Instance.CurrencyResource.ResourceController.ValueChanged += CurrencyValueChanged;
        }

        private void CurrencyValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            UpdateCurrencyValueText((int)e.NewValue);
        }

        private void UpdateCurrencyValueText(int newValue)
        {
            ValueText.text = newValue.ToString();
        }
    }
}
