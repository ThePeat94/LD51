using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Slider m_musicVolumeSlider;
        [SerializeField] private Slider m_sfxVolumeSlider;
        [SerializeField] private Animator m_transition;
        [SerializeField] private float m_transitionTime;


        private void Awake()
        {
            this.m_musicVolumeSlider.onValueChanged.AddListener(this.MusicVolumeSliderChanged);
            this.m_sfxVolumeSlider.onValueChanged.AddListener(this.SfxVolumeSliderChanged);
        }

        private void Start()
        {
            this.m_musicVolumeSlider.value = GlobalSettings.Instance.MusicVolume;
            this.m_sfxVolumeSlider.value = GlobalSettings.Instance.SfxVolume;
        }

        public void ShowMenu()
        {
            this.gameObject.SetActive(true);
        }
        
        public void MusicVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.MusicVolume = volume;
        }

        public void QuitApplication()
        {
            GameStateManager.Instance.TriggerGameQuit();
        }

        public void SfxVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.SfxVolume = volume;
        }

        public void BackToMenu()
        {
            GameStateManager.Instance.TriggerBackToMainMenu();
        }

        public void CloseMenu()
        {
            this.gameObject.SetActive(false);
            GameStateManager.Instance.TriggerUnpause();
        }
    }
}