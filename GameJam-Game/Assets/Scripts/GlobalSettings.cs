using System;

namespace Nidavellir
{
    public class GlobalSettings
    {
        private static GlobalSettings s_instance;
    
        private float m_musicVolume;
        private EventHandler m_musicVolumeChanged;
        private float m_sfxVolume;
        private EventHandler m_sfxVolumeChanged;
        private EventHandler m_sensitivityChanged;

        private GlobalSettings()
        {
            this.m_musicVolume = 1f;
            this.m_sfxVolume = 1f;
        }

        public static GlobalSettings Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new GlobalSettings();

                return s_instance;
            }
        }

        public float MusicVolume
        {
            get => this.m_musicVolume;
            set
            {
                this.m_musicVolume = value;
                this.m_musicVolumeChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public float SfxVolume
        {
            get => this.m_sfxVolume;
            set
            {
                this.m_sfxVolume = value;
                this.m_sfxVolumeChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public event EventHandler MusicVolumeChanged
        {
            add => this.m_musicVolumeChanged += value;
            remove => this.m_musicVolumeChanged -= value;
        }

        public event EventHandler SfxVolumeChanged
        {
            add => this.m_sfxVolumeChanged += value;
            remove => this.m_sfxVolumeChanged -= value;
        }
    
        public event EventHandler SensitivityChanged
        {
            add => this.m_sensitivityChanged += value;
            remove => this.m_sensitivityChanged -= value;
        }
    }
}