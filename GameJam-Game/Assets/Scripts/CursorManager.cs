using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class CursorManager : MonoBehaviour
    {
        private static CursorManager s_instance;
        
        private void Awake()
        {
            if (s_instance == null)
            {
                SceneManager.sceneLoaded += this.OnSceneLoaded;
                DontDestroyOnLoad(this.gameObject);
                return;
            }
            
            
            Destroy(this.gameObject);
            
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            var hasLoadedMainMenu = arg0.buildIndex == 0;

            if (hasLoadedMainMenu)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
