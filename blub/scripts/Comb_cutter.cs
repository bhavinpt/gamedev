using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comb_cutter : MonoBehaviour {

    HingeJoint Cutter;
    public int MinLimit, MaxLimit;
	void Start () {
        Cutter = transform.GetChild(0).GetComponent<HingeJoint>();
        Cutter.useMotor = false;
        JointLimits Limits = Cutter.limits;
        Limits.max = MaxLimit;
        Limits.min = MinLimit;
        Cutter.limits = Limits;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bulb"))
        {
            Cutter.useMotor = true;
        }
    }
}
