using UnityEngine;

public class TerrainGeneratorWithPerlinNoise : MonoBehaviour
{
    public int depth = 10;

    public int width = 256;
    public int height = 256;

    public float scale = 5.0f;
    public float perlinScaleX = 2.0f;
    public float perlinScaleY = 4.0f;

    public float radius = 118f;

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateNewTerrain(terrain.terrainData);
    }

    private TerrainData GenerateNewTerrain(TerrainData dataOfTerrain)
    {
        dataOfTerrain.heightmapResolution = width + 1;

        dataOfTerrain.size = new Vector3(width, depth, height); // (x, y, z)

        dataOfTerrain.SetHeights(0, 0, GenerateHeightsWithPerlinNoise());

        return dataOfTerrain;

    }

    private float[,] GenerateHeightsWithPerlinNoise()
    {
        float[,] heights = new float[width, height];
        float centerOffsetX = width / 2f;
        float centerOffsetY = height / 2f;

        for (int indexX = 0; indexX < width; indexX++)
        {
            for (int indexY = 0; indexY < height; indexY++)
            {
                float differenceXToCenter = (indexX - centerOffsetX);
                float differenceYToCenter = (indexY - centerOffsetY);
                if (Mathf.Sqrt(differenceXToCenter * differenceXToCenter + differenceYToCenter * differenceYToCenter) > radius)
                {
                    heights[indexX, indexY] = depth;
                    continue;
                }

                float x = (differenceXToCenter / width) * scale;
                float y = (differenceYToCenter / height) * scale;
                float p = (Mathf.Pow(x, 4) + Mathf.Pow(y, 4)) / 10f;
                heights[indexX, indexY] = Mathf.PerlinNoise(x * perlinScaleX, y * perlinScaleY) * p;
            }
        }

        return heights;
    }
}
