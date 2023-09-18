using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_screw : MonoBehaviour {

    public float StepSize,PushSpeed;
    public int NumOfSteps, CurrentStep;
    public Rigidbody JackRb;
    Vector3 StartPos;

	void Start () {
        if (JackRb==null)
        {
            Debug.Log("forgot to apply jack rigidbody");
        }
        StartPos = JackRb.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 TargetPos = Vector3.MoveTowards(JackRb.position, StartPos + new Vector3(0,CurrentStep * StepSize, 0), Time.deltaTime * PushSpeed);
        JackRb.MovePosition(TargetPos);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pump_exit"))
        {
            CurrentStep = (int)Mathf.Repeat(CurrentStep + 1, NumOfSteps+1);
        }
    }
}
