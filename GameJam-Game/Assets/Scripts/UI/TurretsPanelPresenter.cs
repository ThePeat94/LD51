using System.Collections.Generic;
using Nidavellir.Scriptables;
using Nidavellir.Towers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir.UI
{
    public class TurretsPanelPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask placeableAreaLayerMask;

        [SerializeField] private GameObject turretButtonPrefab;

        [SerializeField] private List<TowerSO> towers;

        private Camera mainCamera;
        private InputAction click;
        private Tower activeTower;
        private MeshRenderer activeTurretMeshRenderer;
        private Color activeTurretOriginalColor;
        private TurretButton selectedTurretButton;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            foreach (var turret in towers)
            {
                var turretButton = Instantiate(turretButtonPrefab, transform).GetComponent<TurretButton>();

                turretButton.SetTurret(turret);
                turretButton.OnButtonClick = towerSo =>
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
                            var activeTurretObject = Instantiate(towerSo.TowerPrefab.gameObject);
                            activeTower = activeTurretObject.GetComponent<Tower>();
                            activeTurretMeshRenderer = activeTurretObject.GetComponentInChildren<MeshRenderer>();
                            activeTurretOriginalColor = activeTurretMeshRenderer.material.color;
                        }
                    }
                    else
                    {
                        var activeTurretObject = Instantiate(towerSo.TowerPrefab.gameObject);
                        activeTower = activeTurretObject.GetComponent<Tower>();
                        activeTurretMeshRenderer = activeTurretObject.GetComponentInChildren<MeshRenderer>();
                        activeTurretOriginalColor = activeTurretMeshRenderer.material.color;
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
                    activeTurretMeshRenderer.material.color = activeTurretOriginalColor;

                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        activeTower.Place();
                        activeTower.Init();
                        activeTower = null;
                    }
                }
                else
                {
                    activeTurretMeshRenderer.material.color = Color.black;
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