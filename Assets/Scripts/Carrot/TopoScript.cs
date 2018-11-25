using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoScript : MonoBehaviour {

    private float t;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public GameObject bunnyenem;
    private GridMove BunnyMove;

    void Start()
    {
        bunnyenem = GameObject.Find("PlayerConejo");
        BunnyMove = bunnyenem.GetComponent<GridMove>();
    }

    void Update()
    {
        if (BunnyMove.inputTouch)
        {
            BunnyMove.inputTouch = false;
            StartCoroutine(moveCarrot(transform));
        }  
    }

    IEnumerator moveCarrot(Transform transform)
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x,startPosition.y * -1, startPosition.z);
        t = 0;
        //Debug.Log(endPosition);
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.z += 180;
        transform.rotation = Quaternion.Euler(rotationVector);

        while (t < 1f)
        {
            t += Time.deltaTime * 1.6f;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        
        yield return 0;
    }



}
