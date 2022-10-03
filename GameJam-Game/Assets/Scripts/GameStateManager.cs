using System;
using Nidavellir.Audio;
using Nidavellir.Input;
using Nidavellir.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        public enum State
        {
            Started,
            Paused,
            Won,
            GameOver
        };

        private static GameStateManager instance;
        
        [SerializeField] private PlayerHud m_playerHud;
        private State m_currentState;
        private InputProcessor m_inputProcessor;
    
        public State CurrentState => this.m_currentState;
        public static GameStateManager Instance => instance;

        public event Action OnPause;
        public event Action OnUnpause;
        public event Action OnQuit;
        public event Action OnValueReset;


        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            
            instance = this;

            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_playerHud = Object.FindObjectOfType<PlayerHud>();
        }
        

        // Update is called once per frame
        void Update()
        {
            if (this.m_inputProcessor.QuitTriggered)
            {
                if(this.m_currentState != State.GameOver && this.m_currentState != State.Won)
                {
                    if (this.m_currentState == State.Started)
                    {
                        TriggerPause();
                    }
                    else if (this.m_currentState == State.Paused)
                    {
                        m_playerHud.HidePauseMenu();
                    }
                }
                else
                {
                    TriggerGameQuit();
                }
                return;
            }

            if (this.m_inputProcessor.BackToMainTriggered)
            {
                TriggerBackToMainMenu();
                return;
            }

            if (this.m_inputProcessor.RetryTriggered)
            {
                TriggerRetry();
            }
        }

        public void TriggerPause()
        {
            this.m_currentState = State.Paused;
            this.m_playerHud.ShowPauseMenu();
            MusicPlayer.Instance.PauseClip();

            Time.timeScale = 0;
            
            OnPause?.Invoke();
        }

        public void TriggerUnpause()
        {
            this.m_currentState = State.Started;
            MusicPlayer.Instance.UnpauseClip();

            Time.timeScale = 1;

            OnUnpause?.Invoke();
        }

        public void TriggerGameOver()
        {
            this.m_currentState = State.GameOver;
            this.m_playerHud.ShowLoseScreen();
        }

        public void TriggerGameWon()
        {
            if(this.m_currentState != State.Won)
            {
                this.m_currentState = State.Won;
                this.m_playerHud.ShowWonScreen();
            }
        }

        public void TriggerGameQuit()
        {
            OnQuit?.Invoke();
            
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                Application.Quit();
        }

        public void TriggerRetry()
        {
            OnValueReset?.Invoke();

            GameStateManager.instance = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void TriggerBackToMainMenu()
        {
            OnValueReset?.Invoke();

            TriggerUnpause();
            GameStateManager.instance = null;
            
            SceneManager.LoadScene(0);
        }
    }
}
