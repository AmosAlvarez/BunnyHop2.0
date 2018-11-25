using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewManager : MonoBehaviour
{

    public bool levelUp = true;

    private Animator anim;

    private GameObject conejo;
    private GameObject canvas;

    public int twoStarJumps;
    public int threeStarJumps;
    public string nextLevel;
    void Start()
    {
        conejo = GameObject.Find("Conejo");
        canvas = GameObject.Find("Canvas");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EndLevel();
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
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("FlipDown");
        yield return new WaitForSeconds(1f);

        levelUp = false;
    }

    IEnumerator startRotationDown(GameObject RespawnTile)
    {
        Debug.Log("Down");
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("FlipUp");
        yield return new WaitForSeconds(1f);

        levelUp = true;
    }

    public void EndLevel()
    {
        Debug.Log("asdasdasd");
        canvas.GetComponent<Animator>().SetTrigger("EndLevel");
        if (conejo.GetComponent<BunnyJump>().jumpCount <= threeStarJumps)
        {
            canvas.GetComponent<Animator>().SetTrigger("Star3");
        }
        else if (conejo.GetComponent<BunnyJump>().jumpCount <= twoStarJumps)
        {
            canvas.GetComponent<Animator>().SetTrigger("Star2");
        }
        else
        {
            canvas.GetComponent<Animator>().SetTrigger("Star1");
        }
        Invoke("NextLevel", 1.5f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RotateWorld()
    {
        if (levelUp)
        {
            levelUp = false;
            anim.SetTrigger("FlipDown");
        }
        else
        {
            levelUp = true;
            anim.SetTrigger("FlipUp");
        }
    }
}
