using UnityEngine;


public class TerrainPainter : MonoBehaviour
{
    public Terrain terrain;

    void Start()
    {
        terrain = GameObject.FindWithTag("Terrain").GetComponent<Terrain>();

        float[,,] map = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
        // Fill(map, 1f);
        Vector3 my_position = transform.position;
        int my_pos_x = Mathf.RoundToInt((my_position.x / (terrain.terrainData.bounds.size.x / terrain.terrainData.alphamapWidth)));
        int my_pos_z = Mathf.RoundToInt((my_position.z / (terrain.terrainData.bounds.size.z / terrain.terrainData.alphamapHeight)));

        for (int x = my_pos_x - 1; x < my_pos_x + 1; x++)
        {
            for (int y = my_pos_z - 1; y < my_pos_z + 1; y++)
            {
                map[y, x, 0] = 0;
                map[y, x, 1] = 1;
            }
        }
        terrain.terrainData.SetAlphamaps(0, 0, map);
    }

    void Fill(float[,,] map, float value)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                // TODO: Add new layers to the soil: 
                // Layer 0: Base layer, gray, unfertile land. Nothing can grow here.
                // Layer 1: Fertile land, grass can grow here
                // Layer 2: Grass layer, grass will suck its power from the layer 1 (when grass grows, 
                //   the fertility of the soil decreases.
                // Other layers? Snow? Water?
                map[x, y, 0] = value;
                map[x, y, 1] = 0;
            }
        }
    }
}
