using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    public int lengthX;
    public int lengthZ;
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;

    public GameObject tilePrefab;
    public GameObject[,] allTiles;

    // Use this for initialization
    void Awake()
    {
        allTiles = new GameObject[lengthX, lengthZ];
        SetUp();
    }

    private void SetUp()    //Construimos el tablero
    {
        for (int i = 0; i < lengthX; i++)
        {
            for (int j = 0; j < lengthZ; j++)
            {
                Vector3 tempPosition = new Vector3(2*i + OffsetX, OffsetY, 2*j + OffsetZ);
                GameObject Tile = Instantiate(tilePrefab, tempPosition, Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
                //Tile.transform.parent = this.transform;
                Tile.name = "( " + i + ", " + j + " )";
                Tile.GetComponent<TileData>().PosX = i;
                Tile.GetComponent<TileData>().PosZ = j;

                allTiles[i, j] = Tile;
            }
        }
    }
}