using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeClimb : MonoBehaviour {

    RopeController RopeMaster;
    bool Climbing;
    public static bool ClimbingStarted;

    void Start () {
        RopeMaster = transform.parent.GetComponent<RopeController>();	
	}
	
	

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bulb") && Climbing == false)
        {
            if (ClimbingStarted == false)
            {
                ClimbingStarted = true;
                RopeMaster.RopeGrabbed();
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("bulb"))
        {
            Climbing = false;
        }
    }
}
