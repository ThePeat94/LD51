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
        [SerializeField] private Slider m_MouseSensitivitySlider;
        [SerializeField] private Animator m_transition;
        [SerializeField] private float m_transitionTime;


        private void Awake()
        {
            this.m_musicVolumeSlider.onValueChanged.AddListener(this.MusicVolumeSliderChanged);
            this.m_sfxVolumeSlider.onValueChanged.AddListener(this.SfxVolumeSliderChanged);
            this.m_MouseSensitivitySlider.onValueChanged.AddListener(this.MouseSensSliderChanged);
        }

        private void Start()
        {
            this.m_musicVolumeSlider.value = GlobalSettings.Instance.MusicVolume;
            this.m_sfxVolumeSlider.value = GlobalSettings.Instance.SfxVolume;
            this.m_MouseSensitivitySlider.value = GlobalSettings.Instance.MouseSensitivity;
        }

        public void ShowMenu()
        {
            Cursor.visible = true;
            this.gameObject.SetActive(true);
        }

        public void MouseSensSliderChanged(float volume)
        {
            GlobalSettings.Instance.MouseSensitivity = volume;
        }

        public void MusicVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.MusicVolume = volume;
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

        public void BackToMenu()
        {
            this.StartCoroutine(this.LoadGame());
        }

        IEnumerator LoadGame()
        {
            this.m_transition.SetTrigger("Start");
            yield return new WaitForSeconds(this.m_transitionTime);
            SceneManager.LoadScene(0);
        }

        public void CloseMenu()
        {
            this.gameObject.SetActive(false);
            Cursor.visible = false;
        }
    }
}