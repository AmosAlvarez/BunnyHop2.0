using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : MonoBehaviour {

    public Vector3 startPosition;
    public Vector3 endPosition;

    public int startTileX;
    public int startTileZ;

    //UNO DE LOS 2 100PRE A 0 XFA ;) 
    public int tileDistanceX;
    public int tileDistanceZ;

    GameObject boardGen;

    GameObject turtleTile;
    GameObject endTile;

    float t;
    float moveSpeed = 6f;
    float gridSize = 4f;

    bool isConejoEncima;

    public GridMove conejo;

    public float speed;

    // Use this for initialization
    void Start () {

        boardGen = GameObject.Find("BoardGen");

        turtleTile = boardGen.GetComponent<BoardGenerator>().allTiles[startTileX, startTileZ];
        endTile = boardGen.GetComponent<BoardGenerator>().allTiles[startTileX + tileDistanceX, startTileZ + tileDistanceZ];

        startPosition = turtleTile.transform.position;
        endPosition = endTile.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (conejo.isOnTortuga)
        {
            Debug.Log(conejo.isOnTortuga);


            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

            // while (t < 1f)
            // {
            //     t += Time.deltaTime * (moveSpeed / gridSize);
            //     transform.position = Vector3.Lerp(startPosition, endPosition, t);
            // }


        }

    }

    void TurtleMove()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    }

}
