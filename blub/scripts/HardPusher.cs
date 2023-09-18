using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPusher : MonoBehaviour {

    public Vector3 ThrowForce , StopPos;
    public float ToggleTime;

    private bool ThrowOn=true;
    Rigidbody Rb;
    Vector3 StartPos , TargetPos;

	void Start () {
        Rb = GetComponent<Rigidbody>();
        StartPos = Rb.position;
        StopPos += StartPos;
        TargetPos = StartPos;
	}
	
	void FixedUpdate () {
        if (ThrowOn)
        {
            Vector3 NextPos = Vector3.MoveTowards(Rb.position, TargetPos, Time.deltaTime * ThrowForce.y);
            Rb.MovePosition(NextPos);
            if (NextPos==TargetPos)
            {
                ThrowOn = false;
                StartCoroutine(Throw());
            }
        }
	}

   /* private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("head"))
        {
            Rigidbody ObjectToThrow = collision.gameObject.GetComponent<Rigidbody>();
            float MassOfObject = ObjectToThrow.mass;
            //ObjectToThrow.AddForce(ThrowForce * MassOfObject);
            ObjectToThrow.velocity = ThrowForce;// * MassOfObject;
        }
    }*/

    IEnumerator Throw()
    {
        yield return new WaitForSeconds(ToggleTime);
        ThrowOn = true;
        TargetPos = (StartPos + StopPos) - TargetPos;
    }
}
