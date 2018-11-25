using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madriguera : MonoBehaviour {

    public GameObject respawnTile;
    public GameObject[,] allTiles;
    public int RespawnTileX;
    public int RespawnTileZ;

    public GameObject conejo;

    public GameObject Mundo;
    private AudioSource audioDig;
    public ParticleSystem digParticle;

    private void Start()
    {
        conejo = GameObject.Find("PlayerConejo");
        Mundo = GameObject.Find("Mundo");
        //allTiles = GameObject.Find("BoardGen").GetComponent<BoardGenerator>().allTiles;
        //respawnTile = allTiles[RespawnTileX, RespawnTileZ];
        audioDig = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pies"))
        {
            digParticle.Play();
            audioDig.Play();
            conejo.GetComponent<Animator>().SetTrigger("Dig");
            conejo.GetComponent<GridMove>().canMove = false;
            Mundo.GetComponent<Manager>().RotWorld(respawnTile);
        }
    }
}
