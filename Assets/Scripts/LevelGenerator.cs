using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int dimension;

    public int[,] islandGrid;

    public GameObject basicUnit;

    // Start is called before the first frame update
    void Start()
    {
        islandGrid = new int[dimension, dimension];

        CreateIsland();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateIsland() {
        for (int i = 0; i < dimension; i++) {
            for (int j = 0; j < dimension; j++) {
                islandGrid[i, j] = Random.Range(0, 2);
            }
        }

        int z = -dimension / 2;

        for (int i = 0; i < dimension; i++) {
            int x = -dimension / 2;
            for (int j = 0; j < dimension; j++) {
                if (islandGrid[i, j] == 1) {
                    float heightRange = Random.Range(0.3f, 0.7f);
                    Instantiate(basicUnit, new Vector3(x, heightRange, z), Quaternion.identity);
                }

                x++;
            }

            z++;
        }
    }
}
