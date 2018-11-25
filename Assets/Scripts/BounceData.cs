using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceData : MonoBehaviour {

    public GameObject[,] allTiles;

    public GameObject bounceTile;
    public int StartTileX;
    public int StartTileZ;

    public void Start()
    {
        allTiles = GameObject.Find("BoardGen").GetComponent<BoardGenerator>().allTiles;
        bounceTile = allTiles[StartTileX, StartTileZ];
    }
}
