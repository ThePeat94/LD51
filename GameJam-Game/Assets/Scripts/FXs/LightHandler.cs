using System.Collections;
using System.Collections.Generic;
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
        [SerializeField, Range(0, 10)] private float timeOfDay;

        void Start()
        {
            if (Application.isPlaying)
            {
                TimerSystem.Instance.OnTimerEndTick += OnTimerEndTick;
                timeOfDay = 0;
            }
        }

        private void OnTimerEndTick()
        {
            if (TimerSystem.Instance.WaveNumber == 6)
            {
                StartCoroutine(DayToNight());
            }
        }

        private IEnumerator DayToNight()
        {
            timeOfDay = 0;
            while (timeOfDay <= 10)
            {
                timeOfDay += 0.01f;
                UpdateLighting(timeOfDay / 10f);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private void Update()
        {
            if (lightingPreset == null)
            {
                return;
            }

            if (!Application.isPlaying)
            {
                UpdateLighting(timeOfDay / 10f);
            }
        }

        private void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = lightingPreset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = lightingPreset.FogColor.Evaluate(timePercent);

            if (directionalLight != null)
            {
                directionalLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
                var angle = lightingPreset.directionalAngle.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3( angle* 360f, angle * 180f, 0));
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