using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir.Audio
{
    /// <summary>
    /// Use this class to randomly play a clip from a given SfxData list.
    /// </summary>
    public class RandomClipPlayer : MonoBehaviour
    {
        [SerializeField] private bool m_concurrentPlyback;
        [SerializeField] private SfxData[] m_audioClips;

        private AudioSource[] m_audioSources;

        private void Awake()
        {

            var audioSourcesCount = this.m_concurrentPlyback ? this.m_audioClips.Length : 1;
            this.m_audioSources = new AudioSource[audioSourcesCount];

            for (var i = 0; i < audioSourcesCount; i++)
            {
                var audioSource = this.gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                this.m_audioSources[i] = audioSource;
            }
        }

        public void PlayRandomOneShot()
        {
            var audioClipIndex = Random.Range(0, this.m_audioClips.Length);
            var audioSourceIndex = this.m_concurrentPlyback ? audioClipIndex : 0;
            this.m_audioSources[audioSourceIndex]
                .PlayOneShot(this.m_audioClips[audioClipIndex]
                    .AudioClip, this.m_audioClips[audioClipIndex]
                    .Volume * GlobalSettings.Instance.SfxVolume);
        }
    }
}