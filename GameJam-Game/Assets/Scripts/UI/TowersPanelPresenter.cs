using System.Collections.Generic;
using Nidavellir.Scriptables;
using Nidavellir.Towers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir.UI
{
    public class TowersPanelPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask placeableAreaLayerMask;

        [SerializeField] private GameObject towerButtonPrefab;

        [SerializeField] private List<TowerSO> towers;

        private Camera mainCamera;
        private InputAction click;
        private Tower activeTower;
        private MeshRenderer activeTowerMeshRenderer;
        private Color activeTowerOriginalColor;
        private TowerButton selectedTowerButton;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            foreach (var towerSo in towers)
            {
                var towerButton = Instantiate(towerButtonPrefab, transform).GetComponent<TowerButton>();

                towerButton.SetTowerSo(towerSo);
                towerButton.OnButtonClick = towerSo =>
                {

                    if (activeTower != null)
                    {
                        Destroy(activeTower.gameObject);
                        if (activeTower.TowerSettings == towerSo)
                        {
                            activeTower = null;
                        }
                        else
                        {
                            var activeTowerObject = Instantiate(towerSo.TowerPrefab.gameObject);
                            activeTower = activeTowerObject.GetComponent<Tower>();
                            activeTowerMeshRenderer = activeTowerObject.GetComponentInChildren<MeshRenderer>();
                            activeTowerOriginalColor = activeTowerMeshRenderer.material.color;
                        }
                    }
                    else
                    {
                        var activeTowerObject = Instantiate(towerSo.TowerPrefab.gameObject);
                        activeTower = activeTowerObject.GetComponent<Tower>();
                        activeTowerMeshRenderer = activeTowerObject.GetComponentInChildren<MeshRenderer>();
                        activeTowerOriginalColor = activeTowerMeshRenderer.material.color;
                    }
                };
            }
        }

        private void Update()
        {
            if (activeTower != null)
            {
                if (GetMouseWorldPosition(out var mouseWorldPosition))
                {
                    mouseWorldPosition.y = 0.5f;
                    activeTower.transform.position = mouseWorldPosition;
                    activeTowerMeshRenderer.material.color = activeTowerOriginalColor;

                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        activeTower.Place();
                        activeTower.Init();
                        activeTower = null;
                    }
                }
                else
                {
                    activeTowerMeshRenderer.material.color = Color.black;
                }
            }
        }

        private bool GetMouseWorldPosition(out Vector3 mouseWorldPosition)
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, placeableAreaLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
                return true;
            }

            mouseWorldPosition = default;
            return false;
        }
    }
}