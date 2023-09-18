using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifting : MonoBehaviour {
    private bool lift,Bouncing;
    Rigidbody Rb,BulbRb;
    Transform Destination;
    public float LiftSpeed;
    Vector3 Init_pos ,TargetPos;

    void Start () {
        Rb = GetComponent<Rigidbody>();
        Destination = transform.parent.GetChild(1);
        Init_pos = transform.parent.GetChild(0).position;
        TargetPos = Init_pos;

        lift = true;
	}

    void FixedUpdate()
    {

        if (lift)
        {
            Vector3 RbMovepos = Vector3.MoveTowards(Rb.position, TargetPos, Time.fixedDeltaTime * LiftSpeed);
            Rb.MovePosition(RbMovepos);
            //Rb.velocity = (TargetPos - Rb.position) * LiftSpeed*Time.deltaTime;
            //Debug.Log("Lifting speed" + Rb.velocity);

            if (Rb.position == TargetPos)
            {
                //Bouncing = true;
                lift = false;
                                            //StartCoroutine(LiftTriggrWait());
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("lift collided");

        if (collision.gameObject.CompareTag("body") || collision.gameObject.CompareTag("head"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
            Debug.Log("lift collided");
                Start_Lift();
            }
        }

    }

        public void LiftTrigger(Vector3 TriggerPos)
    {
        Debug.Log("lifting");
        if (TriggerPos != TargetPos)
        {
            TargetPos = TriggerPos;
            lift = true;
        }
    }
    private void Start_Lift()
    {
        if (lift == false )//&& Bouncing == false)
        {
            TargetPos = (Init_pos + Destination.position) - TargetPos;    // invert target
                                                                          //Debug.Log("currentpos="+transform.position+"dest pos = "+Destination.position+"source pos = "+Init_pos+"target pos = "+TargetPos);
            lift = true;
        }
    }

    /*IEnumerator LiftTriggrWait()
    {
        yield return new WaitForSeconds(2f);
        Bouncing = false;
        
    }*/
}



