using UnityEngine;

public class TerrainGeneratorWithPerlinNoise : MonoBehaviour
{
    public int depth;
    public float scale;
    public float perlinScaleX;
    public float perlinScaleY;
    public float radius;

    private int width = 128;
    private int height = 128;

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
                    heights[indexX, indexY] = depth; // if it is not in the defined circle then ste the maximum height
                    continue;
                }

                float x = (differenceXToCenter / width) * scale;
                float y = (differenceYToCenter / height) * scale;
                float p = (Mathf.Pow(x, 4) + Mathf.Pow(y, 4)) / 10f;
                heights[indexX, indexY] = Mathf.PerlinNoise(x * perlinScaleX, y * perlinScaleY) * p; /// Perlin Noise with parameters and the polynom
            }
        }

        return heights;
    }
}
