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
            TimerSystem.Instance.OnTotalTimeTick += TotalTimerTick;
        }

        private void TotalTimerTick(float totalTime)
        {
            valueText.text = TimeSpan.FromSeconds(totalTime).ToString(@"mm\:ss");
        }
    }
}
