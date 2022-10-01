using UnityEngine;

namespace Nidavellir
{
    public class TimerSystem : MonoBehaviour
    {
        public delegate void TickTimer();
        
        public static TimerSystem Instance;

        public event TickTimer OnTimerTick;
        
        private const float TickerTime = 10f;

        private float time;

        private TimerSystem()
        {
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Update()
        {
            time += Time.deltaTime;

            if (time >= TickerTime)
            {
                time = 0;
                OnTimerTick?.Invoke();
            }
        }
    }
}