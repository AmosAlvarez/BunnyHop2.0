using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSettings : MonoBehaviour {

    bool activado;
    public GameObject settings;

    BunnyJump conejo;

    public void MostrarSettings()
    {
        if (activado == false)
        {
            settings.SetActive(true);
            activado = true;
            Time.timeScale = 0.0f;
            BunnyJump.canJump = false;

        }
        else if (activado == true)
        {
            settings.SetActive(false);
            activado = false;
            Time.timeScale = 1.0f;
            BunnyJump.canJump = true;
        }


    }

}
