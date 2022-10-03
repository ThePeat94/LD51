using UnityEngine;

namespace Nidavellir
{
    public class Rain : MonoBehaviour
    {
        private ParticleSystem rainParticleSystem;
        
        private void Awake()
        {
            rainParticleSystem = GetComponent<ParticleSystem>();
        }

        void Start()
        {
            TimerSystem.Instance.OnTimerEndTick += OnTimerEndTick;
        }

        private void OnTimerEndTick()
        {
            if (TimerSystem.Instance.WaveNumber == 6)
            {
                rainParticleSystem.Play();
            }
        }
    }
}
