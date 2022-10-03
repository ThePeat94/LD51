using System;
using Nidavellir.Input;
using Nidavellir.Towers;
using Nidavellir.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    public class GameCursor : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;
        private PlayerHud m_playerHud;
        private GameStateManager m_gameStateManager;

        private void Awake()
        {
            this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
            this.m_playerHud = FindObjectOfType<PlayerHud>(true);
            this.m_gameStateManager = FindObjectOfType<GameStateManager>(true);
        }

        private void Update()
        {
            if (this.m_gameStateManager.CurrentState != GameStateManager.State.Started)
                return;
            
            if (this.m_inputProcessor.LeftMouseClickTriggered)
            {
                var ray = Camera.main.ScreenPointToRay(this.m_inputProcessor.MousePosition);
                if(Physics.Raycast(ray, out var hitInfo, 100f, 1 << LayerMask.NameToLayer("Clickable")))
                {
                    if (hitInfo.collider.TryGetComponent<Tower>(out var tower) && tower.IsPlaced)
                    {
                       this.m_playerHud.ShowTower(tower);
                    }
                }
            }
        }
    }
}