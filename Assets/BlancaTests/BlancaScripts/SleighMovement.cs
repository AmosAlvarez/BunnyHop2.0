using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighMovement : MonoBehaviour {

    Animator anim;
    public int moveDistance;
    bool isMoving = false;
    GameObject bunny;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        bunny = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isMoving)
            bunny.transform.position = new Vector3(transform.position.x, bunny.transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 1, Mathf.RoundToInt(transform.position.z));

    }

    public void Move()
    {
        if (moveDistance == 0 && !isMoving)
        {
            BunnyJump.canJump = true;
        }
        else
        {
            if (moveDistance > 0)
            {
                isMoving = true;
                anim.Play("SleighMove");
                --moveDistance;
            }
            else
            {
                isMoving = false;
                bunny.SendMessage("JumpOffSleigh");
                anim.Play("SleighStill");
            }
        }    
    }
}
