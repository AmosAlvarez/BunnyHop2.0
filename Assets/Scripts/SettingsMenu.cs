using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource effectsSource;
    bool music = true;
    bool effects = true;

    void Start()
    {
        musicSource.volume = 1;
        effectsSource.volume = 1;
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat ("volume", volume);
    }

    public void MuteMusic()
    {
        if (music == false)
        {
            music = true;
            musicSource.volume = 1;
            
        }else if (music == true)
        {
            music = false;
            musicSource.volume = 0;
           
        }

    }

    public void MuteEffects()
    {
        if (effects == true)
        {
            effects = false;
            effectsSource.volume = 0;

        }
        else if (effects == false)
        {
            effects = true;
            effectsSource.volume = 1;

        }

    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CerrarOpciones()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
