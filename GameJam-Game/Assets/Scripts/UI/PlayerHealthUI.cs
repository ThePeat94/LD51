using System;
using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Resource m_playerHealthResource;
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private float m_currentHealth;
        [SerializeField] private Image m_background;

        private void Awake()
        {
            this.m_playerHealthResource.ResourceController.ValueChanged += this.PlayerHealthChanged;
        }

        private void Start()
        {
            this.m_healthText.text = this.m_playerHealthResource.ResourceController.CurrentValue.ToString();
            m_currentHealth = this.m_playerHealthResource.ResourceController.CurrentValue;
        }

        private void Update()
        {
            if (m_currentHealth <= 8)
            {
                Debug.Log("Low on health!");
                AnimateHealthBarLow();
            }
        }

        private void PlayerHealthChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_healthText.text = e.NewValue.ToString();
            this.m_currentHealth = e.NewValue;
            Debug.Log("m_currentHealth:" + m_currentHealth);
            AnimateHealthBarHit();
        }

        private void AnimateHealthBarHit()
        {
             this.transform.DOShakePosition(0.5f, 5);
             this.transform.DOShakeScale(0.5f, 0.1f, randomnessMode:ShakeRandomnessMode.Harmonic);
             // this.m_background.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
             this.m_healthText.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
        }

        private void AnimateHealthBarLow()
        {
            this.transform.DOShakePosition(-1, 6);
            this.transform.DOShakeScale(0.5f, 0.1f, vibrato:5, randomnessMode:ShakeRandomnessMode.Harmonic);
            this.m_healthText.DOColor(Color.red, 0.25f);
        }


    }
}