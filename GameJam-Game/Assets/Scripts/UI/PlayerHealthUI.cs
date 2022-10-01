using System;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Resource m_playerHealthResource;
        [SerializeField] private TextMeshProUGUI m_healthText;

        private void Awake()
        {
            this.m_playerHealthResource.ResourceController.ValueChanged += this.PlayerHealthChanged;
        }

        private void Start()
        {
            this.m_healthText.text = this.m_playerHealthResource.ResourceController.CurrentValue.ToString();
        }

        private void PlayerHealthChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_healthText.text = e.NewValue.ToString();
        }
    }
}