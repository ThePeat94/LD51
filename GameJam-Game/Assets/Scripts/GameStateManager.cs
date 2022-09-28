using Nidavellir.Input;
using Nidavellir.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        public enum State
        {
            Started,
            Paused
        };

        private State m_currentState;
        private InputProcessor m_inputProcessor;
    
        public State CurrentState => this.m_currentState;


        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (this.m_inputProcessor.QuitTriggered)
            {
                this.m_currentState = State.Paused;
                FindObjectOfType<PauseMenu>(true).ShowMenu();
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
    }
}
