using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarrotCorrect : MonoBehaviour {

    public ParticleSystem stars;
    public AudioSource bite;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stars.Play();
            bite.Play();
            other.gameObject.transform.root.gameObject.GetComponent<GridMove>().Victory = true;
            Destroy(this.gameObject.transform.parent.gameObject);  
        }
    }

}
