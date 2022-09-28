using UnityEngine;

namespace Nidavellir.Scriptables.Audio
{
    /// <summary>
    /// Use this Scriptable Object to configure your Music Tracks independently from each other. Each available Music Track in your Assets should have one Music Data SO.
    /// </summary>
    [CreateAssetMenu(fileName = "Music Data", menuName = "Music Data", order = 0)]
    public class MusicData : ScriptableObject
    {
        [SerializeField] private AudioClip m_musicClip;
        [SerializeField, Range(0f, 1f)] private float m_volume = 1f;
        
        /// <summary>
        /// Sets, if the given Clip will be looped or not. Will have no effect when a following clip is set.
        /// </summary>
        [SerializeField] private bool m_looping;
        
        /// <summary>
        /// With this you can configure a series of Music Tracks but also create music loops.
        /// </summary>
        [SerializeField] private MusicData m_followingClip;

        public AudioClip MusicClip => this.m_musicClip;
        public float Volume => this.m_volume;
        public bool Looping => this.m_looping;
        public MusicData FollowingClip => this.m_followingClip;
    }
}