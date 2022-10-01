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
            totalTime += Time.deltaTime;

            OnTimerTick?.Invoke(time);
            OnTotalTimeTick?.Invoke(totalTime);

            if (time >= TickerTime)
            {
                time = 0;
                OnTimerEndTick?.Invoke();
            }
        }
    }
}