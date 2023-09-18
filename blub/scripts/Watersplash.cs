using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watersplash : MonoBehaviour {
    Rigidbody Rb;
    private bool Sinking;
    public Vector3 Centeroffset;
    public float WaterLevel, FloatHeight, BounceDamp;
    public float ForceFactor;
    public Vector3 ActionPoint, UpLift;
    
    void Start () {
        BoxCollider Box = GetComponent<BoxCollider>();

        //WaterLevel += transform.position.y + Box.bounds.extents.y+Mathf.Abs(Box.center.y);
		
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y-WaterLevel, transform.position.z), Vector3.one*0.3f);
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (Sinking)
        {
            ActionPoint = Rb.position + transform.TransformDirection(Centeroffset);
            ForceFactor = 1f - ((Rb.position.y - transform.position.y + WaterLevel) / FloatHeight);

            if (ForceFactor > 0f)       // below water
            {
                UpLift = -Physics.gravity * (ForceFactor - Rb.velocity.y * BounceDamp);
                Rb.AddForceAtPosition(UpLift,ActionPoint);
            }

        }
	}

    /*private void OnTriggerStay(Collider other)
    {
        Rb = other.GetComponent<Rigidbody>();
        //ActionPoint = Rb.position + transform.TransformDirection(Centeroffset);
        //ForceFactor = 1f - ((Rb.position.y - transform.position.y + WaterLevel) / FloatHeight);

            Rb.AddForce(new Vector3(0,WaterLevel,0),ForceMode.VelocityChange);

    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("body")||other.CompareTag("head"))
        {
            Debug.Log(" water fall");
            Rb = other.GetComponent<Rigidbody>();
            if (other.transform.parent.GetComponent<Bulb_movement>())
            {
                Debug.Log("rrr");
                other.transform.parent.GetComponent<Bulb_movement>().enabled = false;
                // other.transform.parent.GetComponent<Double_bulb>().enabled = false;
            }
            UnfreezeRotations(Rb);

            if (other.CompareTag("body"))
            {
                Rigidbody HeadRb = Rb.transform.parent.GetChild(0).GetComponent<Rigidbody>();
                if (HeadRb.GetComponent<FixedJoint>())
                {
                    UnfreezeRotations(HeadRb);
                }
            }
            else if (other.CompareTag("head") && other.GetComponent<FixedJoint>())
            {
                Rigidbody BodyRb = Rb.transform.parent.GetChild(1).GetComponent<Rigidbody>();
                UnfreezeRotations(BodyRb);
            }
            Sinking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bulb"))
        {
            Sinking = false;
        }
    }

    void UnfreezeRotations(Rigidbody TempRb)
    {
        TempRb.constraints = RigidbodyConstraints.None;
        TempRb.constraints = RigidbodyConstraints.FreezePositionZ;
    }
}
