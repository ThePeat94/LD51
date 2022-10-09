using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class ApplicationHandler : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        }
    }
}