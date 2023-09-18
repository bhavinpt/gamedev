using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press_detection_Slave : MonoBehaviour {

    public bool Hitting;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("body") || collision.gameObject.CompareTag("head")) 
        {
            Hitting = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("body") || collision.gameObject.CompareTag("head"))
        {
            Hitting = false ;
        }
    }
}
