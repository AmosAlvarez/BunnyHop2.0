using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public GameObject bunny;
    public GameObject buttonRight;
    public GameObject buttonLeft;

    private GridMove movCon;
    private bool rotating;
    private float value = 90;

    public bool posicion0 = true;
    public bool posicion1;
    public bool posicion2;
    public bool posicion3;


    // Use this for initialization
    void Start () {
        //bunny = GameObject.Find("PlayerConejo");
        //movCon = bunny.GetComponent<GridMove>();
	}

    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        transform.rotation = endRotation;
        rotating = false;
        //movCon.canMove = true;
        buttonRight.SetActive(true);
        buttonLeft.SetActive(true);
    }

    public void StartRotation()
    {
        if (!rotating)
            StartCoroutine(Rotate(new Vector3(0, value, 0), 1));
    }

    public void RightTurn()
    {
        //movCon.canMove = false;
        buttonRight.SetActive(false);
        buttonLeft.SetActive(false);
        value = -90;
        StartRotation();

        if (posicion0) {

            posicion1 = true;
            posicion0 = false;

        }else if (posicion1)

        {
            posicion2 = true;
            posicion1 = false;

        }else if (posicion2)

        {
            posicion3 = true;
            posicion2 = false;

        }else if (posicion3)

        {
            posicion0 = true;
            posicion3 = false;
        }

    }

    public void LeftTurn()
    {
        //movCon.canMove = false;
        buttonRight.SetActive(false);
        buttonLeft.SetActive(false);
        value = 90;
        StartRotation();

        if (posicion0)
        {
            posicion3 = true;
            posicion0 = false;

        }else if (posicion1)

        {
            posicion0 = true;
            posicion1 = false;

        }else if (posicion2)

        {
            posicion1 = true;
            posicion2 = false;

        }else if (posicion3)

        {
            posicion2 = true;
            posicion3 = false;
        }
    }
}
