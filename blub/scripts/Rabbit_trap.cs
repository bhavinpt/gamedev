using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_trap : MonoBehaviour {

    private Rigidbody DownBaseStick, SupportStick;
    private bool Triggered;
    HingeJoint LastChainJoint;

	void Start () {
        DownBaseStick = transform.GetChild(6).GetComponent<Rigidbody>();
        SupportStick = transform.GetChild(8).GetComponent<Rigidbody>();
        DownBaseStick.isKinematic = true;
        SupportStick.isKinematic = true;
        LastChainJoint = transform.GetChild(8).GetChild(0).GetChild(10).GetComponent<HingeJoint>();
    }
	

    private void OnTriggerEnter(Collider col)
    {
        if (!Triggered)
        {
            DownBaseStick.isKinematic = false;
            SupportStick.isKinematic = false;
            Triggered = true;
            Destroy(LastChainJoint.gameObject);
        }
        
    }
}
