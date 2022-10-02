using System;
using UnityEngine;
using System.Linq;

namespace Nidavellir.PathManagement
{
    public class PathPainter : MonoBehaviour
    {
        [SerializeField] private Terrain m_terrain;
        [SerializeField] private Path m_path;

        [ContextMenu("Paint Path")]
        public void PaintTexture()
        {
            var terrainData = Terrain.activeTerrain.terrainData;
            var alphaMaps = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

            for (int width = 0; width < terrainData.alphamapWidth; width++)
            {
                for (int height = 0; height < terrainData.alphamapHeight; height++)
                {
                    alphaMaps[width, height, 0] = 1f;
                }
            }

            foreach (var wayPoint in this.m_path.WayPoints)
            {
                Vector3 splatPosition = Vector3.zero;
                splatPosition.x = ((wayPoint.position.x - Terrain.activeTerrain.transform.position.x) / terrainData.size.x) * terrainData.alphamapWidth;
                splatPosition.z = ((wayPoint.position.z - Terrain.activeTerrain.transform.position.z) / terrainData.size.z) * terrainData.alphamapHeight;
            }

            terrainData.SetAlphamaps(0, 0, alphaMaps);
        }
    }
}