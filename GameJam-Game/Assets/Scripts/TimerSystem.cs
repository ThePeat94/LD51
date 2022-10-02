using UnityEngine;

namespace Nidavellir
{
    public class TimerSystem : MonoBehaviour
    {
        public static TimerSystem Instance;

        public delegate void TickTimerEnd();
        public delegate void TickTotalTime(float totalTime);
        public delegate void TickTimer(float timerTick);
        
        public event TickTimerEnd OnTimerEndTick;
        public event TickTotalTime OnTotalTimeTick;
        public event TickTimer OnTimerTick;
        
        private const float TickerTime = 10f;

        private float totalTime;
        private float tickTime;

        public float TotalTime => totalTime;
        public float TickTime => tickTime;

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
            tickTime += Time.deltaTime;
            totalTime += Time.deltaTime;

            OnTimerTick?.Invoke(tickTime);
            OnTotalTimeTick?.Invoke(totalTime);

            if (tickTime >= TickerTime)
            {
                tickTime = 0;
                OnTimerEndTick?.Invoke();
            }
        }
    }
}