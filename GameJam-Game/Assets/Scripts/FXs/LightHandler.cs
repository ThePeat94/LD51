using DG.Tweening;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    [ExecuteAlways]
    public class LightHandler : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [SerializeField] private LightingPreset lightingPreset;
        [SerializeField, Range(0, 24)] private float timeOfDay;

        void Start()
        {
            if (Application.isPlaying)
            {
                TimerSystem.Instance.OnTimerEndTick += OnTimerEndTick;
            }
        }

        private void OnTimerEndTick()
        {
            if (TimerSystem.Instance.WaveNumber == 1)
            {
            }
        }

        private void Update()
        {
            if (lightingPreset == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                timeOfDay += Time.deltaTime;
                timeOfDay %= 24;

                UpdateLighting(timeOfDay / 24f);
            }
            else
            {
                UpdateLighting(timeOfDay / 24f);
            }
        }

        private void UpdateLighting(float timePercent)
        {
            // RenderSettings.ambientLight = lightingPreset.AmbientColor.Evaluate(timePercent);

            if (directionalLight != null)
            {
                directionalLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            }
        }

        private void OnValidate()
        {
            if (directionalLight == null)
            {
                return;
            }

            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
            else
            {
                directionalLight = GetComponentInChildren<Light>();
            }
        }
    }
}