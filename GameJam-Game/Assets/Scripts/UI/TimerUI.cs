using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class TimerUI : MonoBehaviour
    {
        [Header("References")] public TextMeshProUGUI valueText;
        private int loopCounter = 1;
        private bool isNumberEven = true;

        private void Start()
        {
            UpdateTimerValueText(TimerSystem.Instance.TotalTime);
            TimerSystem.Instance.OnTotalTimeTick += UpdateTimerValueText;
            TimerSystem.Instance.OnTimerEndTick += OnTimerEndTick;

            // // valueText.transform.DORotate(new Vector3(0, 0, 2f), 1f).SetLoops(-1)
            // // .SetEase(Ease.InOutBack);
            // var sequence = DOTween.Sequence();
            // sequence.Append(valueText.transform.DORotate(new Vector3(0, 0, 2f), 1f).SetEase(Ease.InOutSine));
            // sequence.Append(valueText.transform.DORotate(new Vector3(0, 0, -2f), 1f).SetEase(Ease.InOutSine));
            // // sequence.Rewind();
            // sequence.SetLoops(-1);
            // sequence.Play();
        }

        private void OnTimerEndTick()
        {
            Debug.Log("OnTimerEndTick");
            // valueText.transform.DOScale(new Vector3(0, 0, 5f), 1f).SetEase(Ease.InOutBack).Flip();
            var tween = valueText.transform.DOScale(loopCounter*0.05f + 1, 0.3f).SetLoops(2, LoopType.Yoyo);
            this.loopCounter += 1;
            tween.OnComplete(() => tween.Rewind());
        }

        private void UpdateTimerValueText(float totalTime)
        {
            // valueText.DOColor((int)TimerSystem.Instance.TotalTime % 2 == 0 ? Color.green : Color.blue, 0.1f);

            valueText.text = TimeSpan.FromSeconds(totalTime).ToString(@"mm\:ss");
        }
    }
}