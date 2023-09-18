using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_cutter : MonoBehaviour {

    HingeJoint LeftCutter, RightCutter;
    Rigidbody LeftArmRb, RightArmRb;
    public int MaxLimit, MinLimit;
    public float TriggerDelay , SquashingDelay , SquashingSpeed;
    public bool LeftFirst, Squashing;
    bool StartSquashing;

	void Start () {
        Transform LeftArm = transform.GetChild(0);
        Transform RightArm = transform.GetChild(1);

        LeftArmRb = LeftArm.GetChild(0).GetComponent<Rigidbody>();
        RightArmRb = RightArm.GetChild(0).GetComponent<Rigidbody>();

        float ArmWidth = LeftArmRb.GetComponent<Renderer>().bounds.extents.x;

        LeftCutter = LeftArm.GetChild(0).GetComponent<HingeJoint>();
        RightCutter = RightArm.GetChild(0).GetComponent<HingeJoint>();

        JointLimits Limits = LeftCutter.limits;
        Limits.max = MaxLimit;
        Limits.min = MinLimit;
        LeftCutter.limits = Limits;
        RightCutter.limits = Limits;

        LeftCutter.useMotor = false;
        RightCutter.useMotor = false;
    }
    private void FixedUpdate()
    {
        if (StartSquashing)
        {
             LeftArmRb.AddForce(-Vector3.right * SquashingSpeed);
            RightArmRb.AddForce(Vector3.right * SquashingSpeed);
            if (LeftArmRb.position.x<transform.position.x)
            {
                LeftArmRb.isKinematic = true;
            }
            else if (RightArmRb.position.x > transform.position.x)
            {
                RightArmRb.isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bulb")&&StartSquashing==false)
        {
            if (LeftFirst)
            {
                LeftCutter.useMotor = true;
                StartCoroutine(SecondArmTrigger(RightCutter));
            }
            else 
            {
                RightCutter.useMotor = true;
                StartCoroutine(SecondArmTrigger(LeftCutter));
            }
        }
    }

    IEnumerator SecondArmTrigger(HingeJoint SecondCutter)
    {
        yield return new WaitForSeconds(TriggerDelay);
        StartCoroutine(DelayedSquashing());
        SecondCutter.useMotor = true;
    }
    IEnumerator DelayedSquashing()
    {
        yield return new WaitForSeconds(SquashingDelay);
        Destroy(LeftCutter);
        Destroy(RightCutter);
        //LeftArmRb.isKinematic = true;
        //RightArmRb.isKinematic = true;
        LeftArmRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        
        RightArmRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        
        StartSquashing = true;

    }
}
