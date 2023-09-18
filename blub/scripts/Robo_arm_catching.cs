using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_arm_catching : MonoBehaviour {

    public float CatchSpeed;
    Transform BulbTrans,TargetTrans;
    Vector3 StartPos, MidPos, EndPos;
    int CatchObj;
    bool Caught;

	void Start () {
        StartPos = transform.parent.GetChild(0).position;
        MidPos = transform.parent.GetChild(1).position;
        EndPos = transform.parent.GetChild(2).position;
	}
	
	void Update () {
        switch (CatchObj)
        {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, StartPos, Time.deltaTime * CatchSpeed);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, BulbTrans.position, Time.deltaTime * CatchSpeed*4);
                if (Vector3.Distance(transform.position,BulbTrans.position)<0.001)
                {
                    Rigidbody BulbRb = BulbTrans.GetComponent<Rigidbody>();
                    BulbRb.useGravity = false;
                    BulbRb.isKinematic = true;
                    BulbTrans.SetParent(transform);
                    CatchObj = 2;
                }
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, MidPos, Time.deltaTime * CatchSpeed);
                if (Vector3.Distance(transform.position, MidPos) < 0.001)
                {
                    CatchObj = 3;
                }
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, EndPos, Time.deltaTime * CatchSpeed);
                if (Vector3.Distance(transform.position, EndPos) < 0.001)
                {
                    Rigidbody BulbRb = BulbTrans.GetComponent<Rigidbody>();
                    BulbRb.useGravity = true;
                    BulbRb.isKinematic = false;
                    BulbTrans.SetParent(null);
                    CatchObj = 0;
                    Caught = false;
                }
                break;

            default:
                break;
        }
        
	}

    public void Trigger(Transform col)
    {
        if(!Caught)
        {
            CatchObj = 1;
            BulbTrans = col;
            Caught = true;

        }
    }
}
