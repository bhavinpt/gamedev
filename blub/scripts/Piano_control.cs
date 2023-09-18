using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_control : MonoBehaviour {

    public float Press_speed,MaxHeight;
    Rigidbody Rb;
    Vector3 StartPos,EndPos;
    bool TargetReached;

	void Start () {
        Rb = GetComponent<Rigidbody>();
        StartPos = Rb.position;
        EndPos = StartPos;
        EndPos.y += MaxHeight;
    }
	
	void FixedUpdate () {
        if (!TargetReached)
        {
            Vector3 TargetPos = Vector3.MoveTowards(Rb.position, EndPos, Time.fixedDeltaTime * Press_speed);
            Rb.position = TargetPos;
            if (Vector3.Distance(Rb.position, EndPos) < 0.001f)
            {
                TargetReached = true;
            }
        }
        else
        {
            Vector3 TargetPos = Vector3.MoveTowards(Rb.position, StartPos, Time.fixedDeltaTime * Press_speed);
            Rb.position = TargetPos;
            if (Vector3.Distance(Rb.position, StartPos) < 0.001f)
            {
                TargetReached = false;
            }
        }
        
	}
}
