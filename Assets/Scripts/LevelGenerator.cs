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
    public int numberOfIslands;

    public int dimension;

    public int[,] islandGrid;

    public GameObject basicUnit;

    string islandBinary = "";
    List<Islands> islands = new List<Islands>();
    LevelInfo levelInfo = new LevelInfo();
    JsonData islandJson;

    public GameObject rootPrefab, parentPrefab;
    GameObject root, parent;
    void Start()
    {
        dimension = 5;
        islandGrid = new int[dimension, dimension];
        root = Instantiate(rootPrefab, Vector3.zero, Quaternion.identity);
        CreateIsland();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateIsland() {
        for (int k = 0; k < numberOfIslands; k++) {
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


            if (numberOfIslands == 0) {
                parent = Instantiate(parentPrefab, Vector3.zero, Quaternion.identity);
                parent.name = "Island_" + numberOfIslands.ToString();
                parent.transform.SetParent(root.transform);

                float z = -dimension / 2;

                for (int i = 0; i < dimension; i++)
                {
                    float x = -dimension / 2;
                    for (int j = 0; j < dimension; j++)
                    {
                        if (islandGrid[i, j] == 1)
                        {
                            float heightRange = Random.Range(0.3f, 0.7f);
                            GameObject unit = Instantiate(basicUnit, new Vector3(x, heightRange, z), Quaternion.identity);
                            unit.transform.SetParent(parent.transform);
                        }

                        x++;
                    }

                    z++;
                }
                CreateIslandConfiguration(Vector3.zero);
            }
            else {
                Vector3 newPos = Random.onUnitSphere * dimension;
                newPos.y = 0f;

                parent = Instantiate(parentPrefab, newPos, Quaternion.identity);
                parent.name = "Island_" + numberOfIslands.ToString();
                parent.transform.SetParent(root.transform);

                float z = newPos.z - dimension / 2;

                for (int i = 0; i < dimension; i++)
                {
                    float x = newPos.x - dimension / 2;
                    for (int j = 0; j < dimension; j++)
                    {
                        if (islandGrid[i, j] == 1)
                        {
                            float heightRange = Random.Range(0.3f, 0.7f);
                            GameObject unit = Instantiate(basicUnit, new Vector3(x, heightRange, z), Quaternion.identity);
                            unit.transform.SetParent(parent.transform);
                        }

                        x++;
                    }

                    z++;
                }
                CreateIslandConfiguration(newPos);

            }

        }

        WriteIslandToFile();
    }

    void CreateIslandConfiguration(Vector3 position) { 
        Islands newIsland = new Islands();
        newIsland.position = position;
        newIsland.islandConfiguration = islandBinary;

        islands.Add(newIsland);
    }

    void WriteIslandToFile() { 
        levelInfo.numOfIslands = numberOfIslands;
        levelInfo.dimensions = dimension;
        levelInfo.islands = islands;
        islandJson = JsonMapper.ToJson(levelInfo);
        File.WriteAllText(Application.dataPath + "/level.json", islandJson.ToString());
    }

    Vector3 RandomVector() {
        return Vector3.zero;
    }
}



