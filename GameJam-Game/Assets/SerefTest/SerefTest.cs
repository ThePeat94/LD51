using UnityEngine;

namespace Nidavellir
{
    public class SerefTest : MonoBehaviour
    {
        private void Start()
        {
            TimerSystem.Instance.OnTimerTick += OnTimerTick;
        }

        private void OnTimerTick()
        {
            Debug.Log($"{nameof(SerefTest)} {nameof(OnTimerTick)} {Time.time}");
        }
    }
}