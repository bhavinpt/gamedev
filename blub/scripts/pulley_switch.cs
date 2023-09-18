using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulley_switch : MonoBehaviour {

    public Rigidbody Door;
    public float Door_Rot_Speed;
    public Vector3 TargetRot_in_Degrees;

    private Quaternion TargetRot_in_Quaternion;
    private void Start()
    {
        if (Door==null)
        {
            Debug.Log("unassigned door in pulley switch");
        }

        TargetRot_in_Quaternion.eulerAngles = TargetRot_in_Degrees;
    }

    private void OnTriggerStay(Collider other)
    {
        bool WithBody = false;

        if (other.CompareTag("body"))
        {
            if (Input.GetKey(KeyCode.P))
            {   // rotates handle
                WithBody = true;
                transform.Rotate(new Vector3(0  , 3f, 0));  
                // rotates door
                Door.rotation = Quaternion.RotateTowards(Door.rotation, TargetRot_in_Quaternion, Time.deltaTime * Door_Rot_Speed);
            }
        }

        else if (other.CompareTag("head") && !WithBody)
        {

            if (Input.GetKey(KeyCode.P))
            {   // rotates handle
                transform.Rotate(new Vector3(0, 3f, 0));
                // rotates door
                Door.rotation = Quaternion.RotateTowards(Door.rotation, TargetRot_in_Quaternion, Time.deltaTime * Door_Rot_Speed);
            }
        }

    }
}
