using UnityEngine;

namespace Nidavellir
{
    public class SerefTest1 : MonoBehaviour
    {
        private void Start()
        {
            TimerSystem.Instance.OnTimerTick += OnTimerTick;
        }

        private void OnTimerTick()
        {
            Debug.Log($"{nameof(SerefTest1)} {nameof(OnTimerTick)} {Time.time}");
        }
    }
}