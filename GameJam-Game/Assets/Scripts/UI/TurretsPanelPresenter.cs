using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir
{
    public class TurretsPanelPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask placeableAreaLayerMask;

        [SerializeField] private GameObject turretButtonPrefab;

        [SerializeField] private List<Turret> turrets;

        private Camera mainCamera;
        private InputAction click;
        private Turret activeTurret;
        private MeshRenderer activeTurretMeshRenderer;
        private Color activeTurretOriginalColor;
        private TurretButton selectedTurretButton;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            foreach (var turret in turrets)
            {
                var turretButton = Instantiate(turretButtonPrefab, transform).GetComponent<TurretButton>();

                turretButton.SetTurret(turret);
                turretButton.OnButtonClick = turretType =>
                {
                    var selectedTurret = turrets.Find(turret => turret.Type == turretType);

                    if (activeTurret != null)
                    {
                        Destroy(activeTurret.gameObject);
                        if (activeTurret.Type == selectedTurret.Type)
                        {
                            activeTurret = null;
                        }
                        else
                        {
                            var activeTurretObject = Instantiate(selectedTurret.gameObject);
                            activeTurret = activeTurretObject.GetComponent<Turret>();
                            activeTurretMeshRenderer = activeTurretObject.GetComponent<MeshRenderer>();
                            activeTurretOriginalColor = activeTurretMeshRenderer.material.color;
                        }
                    }
                    else
                    {
                        var activeTurretObject = Instantiate(selectedTurret.gameObject);
                        activeTurret = activeTurretObject.GetComponent<Turret>();
                        activeTurretMeshRenderer = activeTurretObject.GetComponent<MeshRenderer>();
                        activeTurretOriginalColor = activeTurretMeshRenderer.material.color;
                    }
                };
            }
        }

        private void Update()
        {
            if (activeTurret != null)
            {
                if (GetMouseWorldPosition(out var mouseWorldPosition))
                {
                    mouseWorldPosition.y = 0.5f;
                    activeTurret.transform.position = mouseWorldPosition;
                    activeTurretMeshRenderer.material.color = activeTurretOriginalColor;

                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        activeTurret = null;
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