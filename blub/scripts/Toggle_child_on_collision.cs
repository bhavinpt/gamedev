using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_child_on_collision : MonoBehaviour {

    public GameObject E_Light;
    public float OnDelay;


    void Start () {
        if (E_Light==null)
        {
            Debug.Log("forgot to set light target");
        }	
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            E_Light.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            StartCoroutine(Delayed_Activation());
        }
    }

    IEnumerator Delayed_Activation()
    {
        yield return new WaitForSeconds(OnDelay);
        E_Light.SetActive(true);

    }
}
