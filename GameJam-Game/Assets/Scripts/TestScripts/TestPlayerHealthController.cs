using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    public class TestPlayerHealthController : MonoBehaviour
    {
        [SerializeField] private PlayerHealthController m_playerHealthController;

        private InputProcessor m_inputProcessor;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
        }

        private void Update()
        {
            if (this.m_inputProcessor.InteractTriggered)
                this.m_playerHealthController.TakeDamage(5);
        }
    }
}
