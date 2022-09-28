using UnityEngine;

namespace Nidavellir.Scriptables.Audio
{
    [CreateAssetMenu(fileName = "SfxData", menuName = "Sfx Data", order = 0)]
    public class SfxData : ScriptableObject
    {
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private float m_volume = 1f;

        public AudioClip AudioClip => this.m_audioClip;
        public float Volume => this.m_volume;
    }
}