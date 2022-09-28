using Nidavellir.Scriptables.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private SfxData m_clickSfx;

        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(this.PlayClickSfx);
        }

        private void PlayClickSfx()
        {
            AudioSource.PlayClipAtPoint(
                this.m_clickSfx.AudioClip, 
                Camera.main.transform.position,
                GlobalSettings.Instance.SfxVolume * this.m_clickSfx.Volume
            );
        }
    }
}