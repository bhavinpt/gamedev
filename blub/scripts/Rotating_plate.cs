using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating_plate : MonoBehaviour {

    Rigidbody Rb;
    public float Rotation_Torque;
    public bool SecondaryArm;
    Vector3 Angular_velocity;
    Transform LocalPos;
    public bool Plate;

    void Start () {
        Angular_velocity = new Vector3(0, 0, Rotation_Torque);
        Rb = GetComponent<Rigidbody>();
        if (SecondaryArm)
        {
            LocalPos = transform.parent.GetChild(0);
        }
        
	}
	
	
	void FixedUpdate () {
        Quaternion Rot = Quaternion.Euler(Angular_velocity * Time.deltaTime);
        Rb.MoveRotation(Rb.rotation*Rot);
        Rb.MovePosition(LocalPos.position);
        //Rb.AddRelativeTorque(Vector3.forward*Rotation_Torque);	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bulb") && Plate==true)
        {
            other.transform.SetParent(transform);
        }
    }
}
