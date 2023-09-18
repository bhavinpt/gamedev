using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robo_arm_move : MonoBehaviour
{
    /*
        private Transform[] Robo_parts;
        private Quaternion[] Init_part_rotation, Target_rotation;
        int Part_count;
        public float MoveSpeed;
        private bool Caught;
        Transform CaughtBulb;
        */

    Robo_arm_catching TargetArm;

    private void Start()
    {
        TargetArm = transform.GetChild(3).GetComponent<Robo_arm_catching>();
        Debug.Log(transform.GetChild(3).name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bulb")||other.CompareTag("inactive_bulb"))
        {
            TargetArm.Trigger(other.transform);
        }
    }
}

/*
    Vector3 CatchPos;
    private void Awake()
    {
        CatchPos = transform.GetChild(0).position;
        //Destroy(transform.GetChild(0).gameObject);
    }

    void Start() {
    

        Robo_parts = GetComponentsInChildren<Transform>();

        Part_count = Robo_parts.Length;
        Init_part_rotation = new Quaternion[Part_count];
        Target_rotation = new Quaternion[Part_count];


        for(int i = 0; i < Part_count; i++)
        {
            Init_part_rotation[i] = Robo_parts[i].localRotation;
            Target_rotation[i] = Quaternion.identity;
        }
	}
	
	void Update ()
    {
        for (int i = 1; i < Part_count; i++)
        {
            Robo_parts[i].localRotation = Quaternion.RotateTowards(Robo_parts[i].localRotation, Target_rotation[i], Time.deltaTime * MoveSpeed);
        }

        if (Robo_parts[4].localRotation == Target_rotation[4])       // reached target rotation
        {   
            if (Target_rotation[4] == Quaternion.identity)  // target was default pos.
            {
                //Debug.Log("to identity");
                for (int i = 1; i < Part_count; i++)
                {
                    Debug.Log(Robo_parts[i].localRotation+" name = "+Robo_parts[i].name);
                    Target_rotation[i] = Init_part_rotation[i];
                }
                if (Caught)
                {
                    CaughtBulb.GetComponent<Rigidbody>().isKinematic = false;
                    CaughtBulb.GetComponent<Rigidbody>().useGravity = true;
                    CaughtBulb.SetParent(null);
                    Caught = false;
                }
            }
            else                                            // target was catching pos
            {
        //        Debug.Log("to pose");
                for (int i = 1; i < Part_count; i++)
                {
//                    Debug.Log(Robo_parts[i].localRotation + " name = " + Robo_parts[i].name);

                    Target_rotation[i] = Quaternion.identity;
                }
            }
            

        }
        if (Caught)
            {
                CaughtBulb.position = Vector3.MoveTowards(CaughtBulb.position, CatchPos, Time.deltaTime * 200);
            }
           
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("bulb") && Robo_parts[4].localRotation==Init_part_rotation[4] &&Caught==false)
        {

            CaughtBulb = other.transform;
            CaughtBulb.GetComponent<Rigidbody>().isKinematic = true;
            CaughtBulb.GetComponent<Rigidbody>().useGravity = false;
            //CaughtBulb.position = CatchPos;
            CaughtBulb.SetParent(Robo_parts[Part_count - 1].transform);
            Caught = true;
        }
    }
    
    IEnumerator Catch_delay()
    {
        yield return new WaitForSeconds(2f);
        Catching = true;
    }
    
}*/
