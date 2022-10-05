using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class TimerSystem : MonoBehaviour
    {
        public static TimerSystem Instance;
        private bool m_started;

        public delegate void TickTimerEnd();
        public delegate void TickTotalTime(float totalTime);
        public delegate void TickTimer(float timerTick);
        
        public event TickTimerEnd OnTimerEndTick;
        public event TickTotalTime OnTotalTimeTick;
        public event TickTimer OnTimerTick;
        
        public const float TickerTime = 10f;

        [SerializeField] private SfxData tickSfxData;

        private float totalTime;
        private float tickTime;
        private int waveNumber;

        public float TotalTime => totalTime;
        public float TickTime => tickTime;
        public int WaveNumber => waveNumber;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                GameStateManager.Instance.OnValueReset += Reset;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        
        private void Reset()
        {
            GameStateManager.Instance.OnValueReset -= Reset;

            Destroy(this);
        }

        public void StartTimer()
        {
            m_started = true;
        }

        private void FixedUpdate()
        {
            if (!this.m_started)
                return;
            
            tickTime += Time.fixedDeltaTime;
            totalTime += Time.fixedDeltaTime;

            OnTimerTick?.Invoke(tickTime);
            OnTotalTimeTick?.Invoke(totalTime);

            if (tickTime >= TickerTime)
            {
                SfxPlayer.Instance.PlayOneShot(tickSfxData);
                tickTime = 0;
                waveNumber++;
                OnTimerEndTick?.Invoke();
            }
        }
    }
}