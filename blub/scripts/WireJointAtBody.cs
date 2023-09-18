using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireJointAtBody : MonoBehaviour {

    Transform Centerpoint;
    Rigidbody Rb;
    public float Stiffnes;

    void Start () {
        Centerpoint = transform.parent.GetChild(0);
        Rb = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {
        if (true)
        {

        }
        Vector3 NextPos = Vector3.MoveTowards(Rb.position, Centerpoint.position, Time.fixedDeltaTime * Stiffnes);
        Rb.MovePosition(NextPos);
	}
}
