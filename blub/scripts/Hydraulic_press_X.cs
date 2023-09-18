using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydraulic_press_X : MonoBehaviour {


    public Rigidbody Hydro_press;
    public float Force;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Hydro_press.velocity=new Vector3(Force,0,0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body")) 
        {
            if (other.transform.position.x < Hydro_press.transform.position.x)  // on right of press
            {
                Force = - Mathf.Abs(Force);
            }
            else                                     // on left of press
            {
                Force = Mathf.Abs(Force);
            }
            //Keep_pressing = true;
        }
    }
}
