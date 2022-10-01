using System;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class TimerUI : MonoBehaviour
    {
        [Header("References")] 
        public TextMeshProUGUI valueText;

        private void Start()
        {
            UpdateTimerValueText(TimerSystem.Instance.TotalTime);
            TimerSystem.Instance.OnTotalTimeTick += UpdateTimerValueText;
        }

        private void UpdateTimerValueText(float totalTime)
        {
            valueText.text = TimeSpan.FromSeconds(totalTime).ToString(@"mm\:ss");
        }
    }
}
