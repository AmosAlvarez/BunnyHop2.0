using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private AudioSource source;
    public AudioClip highlight;
    public AudioClip click;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Highlight()
    {
        source.clip = highlight;
        source.Play();
    }

    public void Play()
    {
        source.clip = click;
        source.Play();
        Destroy(GameObject.Find("MenuAudioManager"));
        SceneManager.LoadScene("Size&ArtTest");
    }

    public void Niveles()
    {
        source.clip = click;
        source.Play();
        SceneManager.LoadScene("Levels");
    }

    public void Controles()
    {
        source.clip = click;
        source.Play();
        SceneManager.LoadScene("Controls");
    }

    public void Exit()
    {
        source.clip = click;
        source.Play();
        Application.Quit();
    }

    public void Return()
    {
        source.clip = click;
        source.Play();
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel(string level)
    {
        source.clip = click;
        source.Play();
        Destroy(GameObject.Find("MenuAudioManager"));
        SceneManager.LoadScene(level);
    }

}
