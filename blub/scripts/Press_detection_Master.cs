using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press_detection_Master : MonoBehaviour {


    public Press_detection_Slave Press;
	// Use this for initialization
	void Start () {
		
	}
	

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("body") || collision.gameObject.CompareTag("head"))
        {
            if (Press.Hitting)
            {
                Debug.Log("died by press");
            }
        }
    }
}
