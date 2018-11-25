using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public bool levelUp = true;

    public GameObject conejo;
    public GameObject skin;
    private Animator anim;
    private GridMove movPlayer;
    void Start () {
        anim = GetComponent<Animator>();
        movPlayer = conejo.GetComponent<GridMove>();
        skin = conejo.transform.GetChild(0).gameObject;
    }
	
	void Update () {
        //RenderSettings.skybox.SetFloat("_Rotation", Time.time);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RotWorld(GameObject RespawnTile)
    {
        if (levelUp)
        {
            StartCoroutine(startRotationUp(RespawnTile));
        }
        else StartCoroutine(startRotationDown(RespawnTile));
    }

    IEnumerator startRotationUp(GameObject RespawnTile)
    {
        Debug.Log("Up");
        yield return new WaitForSeconds(0.7f);
        movPlayer.canMove = false;
        skin.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("FlipDown");
        movPlayer.RabbitTile = RespawnTile;
        movPlayer.EndTile = RespawnTile;
        conejo.transform.position = new Vector3(RespawnTile.transform.position.x, conejo.transform.position.y, RespawnTile.transform.position.z);
        yield return new WaitForSeconds(1f);
        conejo.SetActive(false);
        conejo.SetActive(true);
        movPlayer.canMove = true;
        skin.SetActive(true);
        
        movPlayer.isMoving = false;
        levelUp = false;
    }

    IEnumerator startRotationDown(GameObject RespawnTile)
    {
        Debug.Log("Down");
        yield return new WaitForSeconds(0.7f);
        movPlayer.canMove = false;
        skin.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("FlipUp");
        movPlayer.RabbitTile = RespawnTile;
        movPlayer.EndTile = RespawnTile;
        conejo.transform.position = new Vector3(RespawnTile.transform.position.x, conejo.transform.position.y, RespawnTile.transform.position.z);
        yield return new WaitForSeconds(1f);
        conejo.SetActive(false);
        conejo.SetActive(true);
        movPlayer.canMove = true;
        skin.SetActive(true);

        movPlayer.isMoving = false;
        levelUp = true;
    }
}
