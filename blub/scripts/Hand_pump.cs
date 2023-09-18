using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_pump : MonoBehaviour {

    public Rigidbody PumpTarget;
    public Vector3 Torque;
    HingeJoint Motor;

	// Use this for initialization
	void Start () {
        if (PumpTarget==null || PumpTarget.GetComponent<HingeJoint>())
        {
            Debug.Log("Pump target not set or has no hingejoint");
        }
        Motor = PumpTarget.GetComponent<HingeJoint>();
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("pump_exit")) 
        {
            Debug.Log("pump");
            Motor.useMotor = true;
            //PumpTarget.AddForceAtPosition(Torque, new Vector3(-0.45f, 0, 0));
            //PumpTarget.AddRelativeTorque(Torque,ForceMode.VelocityChange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Motor.useMotor = false;
    }

 
}
