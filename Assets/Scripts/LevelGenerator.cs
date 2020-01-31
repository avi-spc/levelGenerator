using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public struct Islands {
    public Vector3 position { get; set; }
    public string islandConfiguration { get; set; }   
}

public class LevelInfo
{
    public int numOfIslands { get; set; }
    public int dimensions { get; set; }
    public List<Islands> islands;

}

public class LevelGenerator : MonoBehaviour
{
    static int dimension;

    public int[,] islandGrid;

    public GameObject basicUnit;

    string islandBinary = "";
    List<Islands> islands = new List<Islands>();
    LevelInfo levelInfo = new LevelInfo();
    JsonData islandJson;
    void Start()
    {
        dimension = 5;
        islandGrid = new int[dimension, dimension];
        CreateIsland();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateIsland() {
        for (int k = 0; k < 2; k++) {
            islandBinary = "";
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    islandGrid[i, j] = Random.Range(0, 2);

                    if (i == dimension - 1 && j == dimension - 1)
                        islandBinary += islandGrid[i, j];
                    else
                        islandBinary += islandGrid[i, j] + " ";
                }
            }

            int z = -dimension / 2;

            for (int i = 0; i < dimension; i++)
            {
                int x = -dimension / 2;
                for (int j = 0; j < dimension; j++)
                {
                    if (islandGrid[i, j] == 1)
                    {
                        float heightRange = Random.Range(0.3f, 0.7f);
                        Instantiate(basicUnit, new Vector3(x, heightRange, z), Quaternion.identity);
                    }

                    x++;
                }

                z++;
            }

            Islands newIsland = new Islands();
            newIsland.position = Vector3.zero;
            newIsland.islandConfiguration = islandBinary;

            islands.Add(newIsland);

        }

        levelInfo.numOfIslands = 2;
        levelInfo.dimensions = dimension;
        levelInfo.islands = islands;
        islandJson = JsonMapper.ToJson(levelInfo);
        File.WriteAllText(Application.dataPath + "/level.json", islandJson.ToString());
    }
}


