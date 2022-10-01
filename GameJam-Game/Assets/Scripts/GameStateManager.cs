using Nidavellir.Input;
using Nidavellir.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private PlayerHud m_playerHud;

        public enum State
        {
            Started,
            Paused,
            GameOver
        };

        private State m_currentState;
        private InputProcessor m_inputProcessor;
    
        public State CurrentState => this.m_currentState;


        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_playerHud = Object.FindObjectOfType<PlayerHud>();
        }
        

        // Update is called once per frame
        void Update()
        {
            if (this.m_inputProcessor.QuitTriggered)
            {
                if(this.m_currentState != State.GameOver)
                {
                    if (this.m_currentState == State.Started)
                    {
                        this.m_currentState = State.Paused;
                        this.m_playerHud.ShowPauseMenu();
                    }
                    else if (this.m_currentState == State.Paused)
                    {
                        this.m_currentState = State.Started;
                        this.m_playerHud.HidePauseMenu();
                    }
                }
                else
                {
                    Application.Quit();
                }
                return;
            }

            if (this.m_inputProcessor.BackToMainTriggered)
            {
                SceneManager.LoadScene(0);
                return;
            }

            if (this.m_inputProcessor.RetryTriggered)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
        }

        public void TriggerGameOver()
        {
            this.m_currentState = State.GameOver;
            this.m_playerHud.ShowLoseScreen();
        }

        public void HidePauseMenu()
        {
            this.m_currentState = State.Started;
        }
    }
}
