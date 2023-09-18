using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift_trigger : MonoBehaviour {
    lifting LiftScript;
    public bool Destination;

	void Start () {
        LiftScript = transform.parent.GetComponentInChildren<lifting>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            LiftScript.LiftTrigger(transform.position);

        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bulb") && Destination == true)
        {
            LiftScript.ResetTriggerFromDest();
        }
    }*/
}
