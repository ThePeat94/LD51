using System.Collections;
using Nidavellir.Scriptables.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir.Audio
{
    /// <summary>
    /// This component serves as a music player. You can give it any title theme which is played whenever you are in the Main Menu Scene and a Game Theme.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicData m_titleTheme;
        [SerializeField] private MusicData m_gameTheme;
        
        private AudioSource m_audioSource;
        private MusicData m_currentMusicData;
        private Coroutine m_playingCoroutine;

        public static MusicPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                SceneManager.sceneLoaded += this.SceneChanged;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            this.m_audioSource = this.GetComponent<AudioSource>();
            GlobalSettings.Instance.MusicVolumeChanged += this.OnMusicVolumeChanged;
        }

        public void PlayMusicData(MusicData toPlay)
        {
            if (this.m_playingCoroutine != null)
                this.StopCoroutine(this.m_playingCoroutine);

            this.m_playingCoroutine = this.StartCoroutine(this.PlayClipList(toPlay));
        }

        private void OnMusicVolumeChanged(object sender, System.EventArgs e)
        {
            this.m_audioSource.volume = this.m_currentMusicData.Volume * GlobalSettings.Instance.MusicVolume;
        }

        private void PlayClip(MusicData toPlay)
        {
            this.m_audioSource.clip = toPlay.MusicClip;
            this.m_audioSource.volume = toPlay.Volume * GlobalSettings.Instance.MusicVolume;
            this.m_audioSource.loop = toPlay.Looping;
            this.m_audioSource.Play();
        }


        private IEnumerator PlayClipList(MusicData toPlay)
        {
            var current = toPlay;

            while (current != null)
            {
                this.m_currentMusicData = current;
                this.PlayClip(current);
                yield return new WaitForSeconds(current.MusicClip.length);
                current = toPlay.FollowingClip;
            }

            this.m_playingCoroutine = null;
        }

        private void SceneChanged(Scene loadedScene, LoadSceneMode arg1)
        {
            var hasLoadedMainMenu = loadedScene.buildIndex == 0;

            if (this.m_playingCoroutine != null)
                this.StopCoroutine(this.m_playingCoroutine);

            if (hasLoadedMainMenu)
                this.m_playingCoroutine = this.StartCoroutine(this.PlayClipList(this.m_titleTheme));
            else if (loadedScene.buildIndex == 1)
                this.m_playingCoroutine = this.StartCoroutine(this.PlayClipList(this.m_gameTheme));
        }
    }
}