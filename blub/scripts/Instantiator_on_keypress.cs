using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator_on_keypress : MonoBehaviour {

    public GameObject Prefab_To_Instantiate;
    public float InterLaunchDelay;
    private bool CanLaunch=true;

    void Start () {
        CanLaunch = true;
	}
	
	void OnTriggerStay (Collider col) {
        if (col.CompareTag("body"))
        {
            if ( CanLaunch && Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("key launch");
                CanLaunch = false;
                Instantiate(Prefab_To_Instantiate, transform.position, Quaternion.identity);

                StartCoroutine(InterDelay());
            }
        }
	}

    IEnumerator InterDelay()
    {

        yield return new WaitForSeconds(InterLaunchDelay);
        CanLaunch = true;
    }
}
