using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_On_Touch : MonoBehaviour {

    public Transform Door;
    public Vector3 TargetRotation;
    public bool Clockwise_from_top;

    void Start()
    {
        if (Door == null)
        {
            Debug.Log("Did not set target door");
        }
    }
    
    void OpenDoor()
    {
        if (Clockwise_from_top)
        {
            CancelInvoke();
            Debug.Log("repeat STOPPed");

        }
        Debug.Log("repeat");
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("body"))
        {
            Door.GetComponent<Rigidbody>().isKinematic = false;
            Door.GetComponent<HingeJoint>().useMotor = true;

            //InvokeRepeating("OpenDoor", 0, Time.deltaTime);
        }
    }
}
