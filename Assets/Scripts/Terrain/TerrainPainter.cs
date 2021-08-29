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
}
