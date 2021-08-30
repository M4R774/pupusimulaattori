using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public Terrain terrain;
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;
	public float heightVariation;

	public int octaves;
	[Range(0, 1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

	public void GenerateMap()
	{
		float[,] noiseMap = PerlinNoiseMapGenerator.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				noiseMap[y, x] = noiseMap[y, x] * heightVariation;
			}
		}
		terrain.terrainData.SetHeights(0, 0, noiseMap);
	}



	// Clamp the values to positive integers
	void OnValidate()
	{
		if (mapWidth < 1)
		{
			mapWidth = 1;
		}
		if (mapHeight < 1)
		{
			mapHeight = 1;
		}
		if (lacunarity < 1)
		{
			lacunarity = 1;
		}
		if (octaves < 0)
		{
			octaves = 0;
		}
	}

}