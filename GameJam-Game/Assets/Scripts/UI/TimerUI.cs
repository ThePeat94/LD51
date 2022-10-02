using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class TimerUI : MonoBehaviour
    {
        [Header("References")] public TextMeshProUGUI valueText;

        private void Start()
        {
            UpdateTimerValueText(TimerSystem.Instance.TotalTime);
            TimerSystem.Instance.OnTotalTimeTick += UpdateTimerValueText;
            TimerSystem.Instance.OnTimerEndTick += OnTimerEndTick;

            // valueText.transform.DORotate(new Vector3(0, 0, 2f), 1f).SetLoops(-1)
                // .SetEase(Ease.InOutBack);
            var sequence = DOTween.Sequence();
            sequence.Append(valueText.transform.DORotate(new Vector3(0, 0, 2f), 1f).SetEase(Ease.InOutSine));
            sequence.Append(valueText.transform.DORotate(new Vector3(0, 0, -2f), 1f).SetEase(Ease.InOutSine));
            sequence.Append(valueText.transform.DORotate(new Vector3(0, 0, 2f), 1f).SetEase(Ease.InOutSine));
            // sequence.Rewind();
            sequence.SetLoops(-1);
            sequence.Play();
        }

        private void OnTimerEndTick()
        {
            Debug.Log("OnTimerEndTick");
            // valueText.transform.DOScale(new Vector3(0, 0, 5f), 1f).SetEase(Ease.InOutBack).Flip();
            var tween = valueText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetEase(Ease.InOutBack);
            tween.OnComplete(() => tween.Rewind());
        }

        private void UpdateTimerValueText(float totalTime)
        {
            valueText.text = TimeSpan.FromSeconds(totalTime).ToString(@"mm\:ss");
        }
    }
}