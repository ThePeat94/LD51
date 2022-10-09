using System.Collections;
using Nidavellir.Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Audio
{
    /// <summary>
    /// This Component can be referenced in any other component which might play any Sfx. Be it a looping sound or just playing a Sfx oneshot.
    /// </summary>
    public class SfxPlayer : MonoBehaviour
    {
        public static SfxPlayer Instance { get; private set; }

        private AudioSource m_loopingAudioSource;

        private SfxPlayer()
        {
        }

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

            this.StopAllCoroutines();
            
            if(m_loopingAudioSource != null)
                StopLoopingCurrentSfx();
            
            foreach (var audioSource in GetComponents<AudioSource>())
            {
                audioSource.Stop();
                Destroy(audioSource);
            }
            
            Destroy(this);
        }

        public AudioSource PlayLoopingSfx(SfxData sfxData)
        {
            var loopingAudioSource = this.AddComponent<AudioSource>();
            loopingAudioSource.loop = true;
            this.PlayClipOnAudioSource(sfxData, loopingAudioSource);
            return loopingAudioSource;
        }

        public void DestroyAudioSource(AudioSource audioSource)
        {
            Destroy(audioSource);
        }

        public void StopLoopingCurrentSfx()
        {
            this.m_loopingAudioSource.Stop();
        }

        public void PlayOneShot(SfxData sfxData)
        {
            if (sfxData.AudioClip == null)
            {
                Debug.LogError($"SfxData {sfxData.name} has no AudioClip assigned");
                return;
            }
            this.StartCoroutine(this.PlayClipAndDestroySource(sfxData));
        }

        private IEnumerator PlayClipAndDestroySource(SfxData data)
        {
            var audioSource = this.AddComponent<AudioSource>();
            this.PlayClipOnAudioSource(data, audioSource);
            yield return new WaitForSeconds(data.AudioClip.length);
            Destroy(audioSource);
        }

        private void PlayClipOnAudioSource(SfxData sfxData, AudioSource audioSource)
        {
            audioSource.clip = sfxData.AudioClip;
            Debug.Log($"Playing Sfx {sfxData.name} with volume {sfxData.Volume * GlobalSettings.Instance.SfxVolume:F2}");
            audioSource.volume = sfxData.Volume * GlobalSettings.Instance.SfxVolume;
            audioSource.Play();
        }
    }
}