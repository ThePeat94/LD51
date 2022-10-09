using System;
using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class PreperationMusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicData m_preperationMusicData;

        private void Start()
        {
            MusicPlayer.Instance.PlayMusicData(this.m_preperationMusicData);
        }
    }
}