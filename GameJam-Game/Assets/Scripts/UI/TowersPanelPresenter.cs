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

        [SerializeField] private LayerMask nonPlaceableAreaLayerMask;

        [SerializeField] private GameObject towerButtonPrefab;

        [SerializeField] private List<TowerSO> towers;

        private Camera mainCamera;
        private InputAction click;
        private Tower activeTower;
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
                    Debug.Log("Tower button clicked");
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
                        }
                    }
                    else
                    {
                        var activeTowerObject = Instantiate(towerSo.TowerPrefab.gameObject);
                        activeTower = activeTowerObject.GetComponent<Tower>();
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

                if (Physics.Raycast(ray, out var raycastGroundHit, float.MaxValue, placeableAreaLayerMask))
                {
                    var mouseWorldPosition = raycastGroundHit.point;
                    // mouseWorldPosition.y = 0.5f;
                    activeTower.transform.position = mouseWorldPosition;

                    var castAll = Physics.BoxCastAll(mouseWorldPosition, Vector3.one, Vector3.up, Quaternion.identity, float.MaxValue, nonPlaceableAreaLayerMask);
                    if (castAll.Length == 0 && GetTerrainLayer(raycastGroundHit) != 1)
                    {
                        activeTower.SetTowerPlaceable(true);
                        // activeTowerMeshRenderer.material.color = activeTowerOriginalColor;

                        if (Mouse.current.leftButton.wasPressedThisFrame && activeTower.Place())
                        {
                            activeTower.Init();
                            activeTower = null;
                        }
                    }
                    else
                    {
                        activeTower.SetTowerPlaceable(false);
                    }
                }
                else
                {
                    activeTower.SetTowerPlaceable(false);
                }
            }
        }

        private int GetTerrainLayer(RaycastHit raycastGroundHit)
        {
            if (raycastGroundHit.collider.gameObject.TryGetComponent<Terrain>(out var terrain))
            {
                var terrainPosition = raycastGroundHit.point - terrain.transform.position;
                var terrainData = terrain.terrainData;
                var splatMapPosition = new Vector3(terrainPosition.x / terrainData.size.x, 0, terrainPosition.z / terrainData.size.z);

                var x = Mathf.FloorToInt(splatMapPosition.x * terrainData.alphamapWidth);
                var z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

                var alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);

                int primaryIndex = 0;

                for (int i = 0; i < alphaMap.Length; i++)
                {
                    if (alphaMap[0, 0, i] > alphaMap[0,0,primaryIndex])
                    {
                        primaryIndex = i;
                    }
                }

                return primaryIndex;
            }

            return 0;
        }
    }
}