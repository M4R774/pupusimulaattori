using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerraintPaintReset : MonoBehaviour
{
    Terrain terrain;

    // TODO: Add new layers to the soil: 
    // Layer 0: Base layer, gray, unfertile land. Nothing can grow here.
    // Layer 1: Fertile land, grass can grow here
    // Layer 2: Grass layer, grass will suck its power from the layer 1 (when grass grows, 
    //   the fertility of the soil decreases.
    // Other layers? Snow? Water?

    private void OnApplicationQuit()
    {
        float[,,] map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, 2];
        Fill(map, 1);
    }

    void Awake()
    {
        terrain = GetComponent<Terrain>();
        float[,,] map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, 2];
        Fill(map, 1);
    }

    void Fill(float[,,] map, float value)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                map[x, y, 0] = value;
                map[x, y, 1] = 0;
            }
        }
        terrain.terrainData.SetAlphamaps(0, 0, map);
    }
}
