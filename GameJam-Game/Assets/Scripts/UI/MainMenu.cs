using System.Collections;
using Nidavellir.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_startMenu;
        [SerializeField] private GameObject m_credits;
        [SerializeField] private Slider m_musicVolumeSlider;
        [SerializeField] private Slider m_sfxVolumeSlider;
        [SerializeField] private GameObject m_optionsPanel;
        [SerializeField] private Slider m_loadingBar;
        [SerializeField] private GameObject m_loadingBarPanel;
        private AsyncOperation m_loadingOperation;


        private void Awake()
        {
            this.m_musicVolumeSlider.onValueChanged.AddListener(this.MusicVolumeSliderChanged);
            this.m_sfxVolumeSlider.onValueChanged.AddListener(this.SfxVolumeSliderChanged);
        }

        private void Start()
        {
            this.m_musicVolumeSlider.value = GlobalSettings.Instance.MusicVolume;
            this.m_sfxVolumeSlider.value = GlobalSettings.Instance.SfxVolume;
            this.StartCoroutine(this.LoadMainGame());
        }

        public void BackFromCreditsToStart()
        {
            this.m_startMenu.SetActive(true);
            this.m_credits.SetActive(false);
        }

        public void BackToStartFromOptions()
        {
            this.m_optionsPanel.SetActive(false);
            this.m_startMenu.SetActive(true);
        }

        public void MusicVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.MusicVolume = volume;
        }

        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }

        public void QuitApplication()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                Application.Quit();
        }

        public void SfxVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.SfxVolume = volume;
        }

        public void ShowCredits()
        {
            this.m_startMenu.SetActive(false);
            this.m_credits.SetActive(true);
        }

        public void ShowOptions()
        {
            this.m_optionsPanel.SetActive(true);
            this.m_startMenu.SetActive(false);
        }

        public void StartGame()
        {
            this.m_startMenu.SetActive(false);
            this.m_loadingBarPanel.SetActive(true);
            this.m_loadingOperation.allowSceneActivation = true;
        }

        private IEnumerator LoadMainGame()
        {
            this.m_loadingOperation = SceneManager.LoadSceneAsync(1);
            this.m_loadingOperation.allowSceneActivation = false;
            while (!this.m_loadingOperation.isDone)
            {
                this.m_loadingBar.value = this.m_loadingOperation.progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}