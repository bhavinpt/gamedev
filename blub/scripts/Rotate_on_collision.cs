using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_on_collision : MonoBehaviour
{

    public Rigidbody Door;
    public float Door_Rot_Speed;
    public Vector3 TargetRot_in_Degrees;

    private Quaternion TargetRot_in_Quaternion , Reset_Rotation_Quaternion;
    private bool KeepRotating = false;

    private void Start()
    {
        if (Door == null)
        {
            Debug.Log("unassigned door in rotateon collision");
        }
        Reset_Rotation_Quaternion = Door.rotation;      // reset rotation of door
        TargetRot_in_Quaternion.eulerAngles = TargetRot_in_Degrees; // rotation of opened door
    }

    private void Update()
    {
        if (KeepRotating)
        {
            Door.rotation = Quaternion.RotateTowards(Door.rotation, TargetRot_in_Quaternion, Time.deltaTime * Door_Rot_Speed);
        }
        else
        {
            Door.rotation = Quaternion.RotateTowards(Door.rotation, Reset_Rotation_Quaternion, Time.deltaTime * Door_Rot_Speed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("body") || other.gameObject.CompareTag("head"))
        {
            KeepRotating = true;
            // rotates door
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("body") || other.gameObject.CompareTag("head"))
        {
            KeepRotating = false;
            // resets door
        }
    }
}
