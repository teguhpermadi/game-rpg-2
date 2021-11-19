using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("PREFRENCES")]
    public bool Randomized = false;      //BOOL NOT IN TUTORIAL {Do you want the terrain generated each time?}
    public bool Animate = false;         //BOOL NOT IN TUTORIAL {Do you want it to be animated and move?
    public float Speed = 5;             //FLOAT NOT IN TUTORIAL {If animating, how fast?)
    //if Animate is true, it works with colliders (add a rigid body to a cube and place the cube above terrain)

    [Space]

    public int depth = 20;  //height from above

    public int width = 256;     //make a int named width and set it to a default of 256
    public int height = 256;    //make int named height and set it to a default of 256 [Length of terrain]

    public float Scale = 20;

    public float offsetX = 100;
    public float offsetY = 100;


    public void Start()
    {
        offsetX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);
    }


    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();      //for Terrain Data
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        if(Animate == true)
        {
        offsetX += Time.deltaTime * Speed;
        }
    }


    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);      //generate some perlin noise value
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCord = (float)x / width * Scale;
        float yCord = (float)y / height * Scale;

        if (Randomized == true)
        {
            xCord *= offsetX;
            yCord *= offsetY;
        }


        return Mathf.PerlinNoise(xCord, yCord);
    }
}
