using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour {

    public GameObject[] piezas;
    public GameObject[] killZones;
    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isDead)
            {
                isDead = true;
                other.gameObject.transform.root.gameObject.GetComponent<GridMove>().Death = true;
                foreach (GameObject pieza in piezas)
                {
                    pieza.AddComponent<Rigidbody>();

                }
                other.gameObject.transform.root.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                StartCoroutine(RestartLevel(other));
            }
        }
    }

    IEnumerator RestartLevel(Collider other)
    {
        //Destroy(other.gameObject.transform.parent.gameObject);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
