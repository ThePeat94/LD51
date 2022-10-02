using System.Collections.Generic;
using Nidavellir.Scriptables;
using Nidavellir.Towers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir.UI
{
    public class TowersPanelPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask withoutAttackTriggerLayerMask;

        [SerializeField] private LayerMask placeableAreaLayerMask;

        [SerializeField] private LayerMask nonPlaceableAreaLayerMask;

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
                var mousePosition = Mouse.current.position.ReadValue();
                var ray = mainCamera.ScreenPointToRay(mousePosition);

                // var nonPlaceableAreaLayerMask = ~placeableAreaLayerMask;

                if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, withoutAttackTriggerLayerMask) &&
                    Physics.Raycast(ray, out var raycastGroundHit, float.MaxValue, placeableAreaLayerMask))
                {
                    var mouseWorldPosition = raycastGroundHit.point;
                    mouseWorldPosition.y = 0.5f;
                    activeTower.transform.position = mouseWorldPosition;
                    
                    var castAll = Physics.BoxCastAll(mouseWorldPosition, Vector3.one, Vector3.up, Quaternion.identity, float.MaxValue, nonPlaceableAreaLayerMask);
                    if (castAll.Length == 0) // it shouldn't hit anything except ground
                    {
                        activeTowerMeshRenderer.material.color = activeTowerOriginalColor;

                        if (Mouse.current.rightButton.wasPressedThisFrame && activeTower.Place())
                        {
                            activeTower.Init();
                            activeTower = null;
                        }
                    }
                    else
                    {
                        activeTowerMeshRenderer.material.color = Color.black;
                    }
                }
                else
                {
                    activeTowerMeshRenderer.material.color = Color.black;
                }
            }
        }
    }
}