using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path_follower : MonoBehaviour {

    Transform[] Nodes;
    Rigidbody BaseRb;
    int TargetNum;
    LineRenderer Line;

    public bool DrawLine;
    public float MoveSpeed;

	void Start () {
        Nodes = transform.GetComponentsInChildren<Transform>();
        BaseRb = transform.GetChild(0).GetComponent<Rigidbody>();
        TargetNum = 2;
        if (DrawLine)
        {
            Line = GetComponent<LineRenderer>();
            Line.positionCount = Nodes.Length - 2;
            for (int i = 0; i < Line.positionCount; i++)
            {
                Line.SetPosition(i, Nodes[i + 2].position);
            }
        }
	}
	
	void FixedUpdate () {
        Vector3 TargetPos = Vector3.MoveTowards(BaseRb.position, Nodes[TargetNum].position, Time.fixedDeltaTime * MoveSpeed);
        BaseRb.MovePosition(TargetPos);


        /*Vector3 TargetDir = Nodes[TargetNum].position - BaseRb.position;
        BaseRb.AddForce(TargetDir * MoveSpeed,ForceMode.VelocityChange);
        Debug.Log(Vector3.Distance(Nodes[TargetNum].position, BaseRb.position));*/

        if (Vector3.Distance(Nodes[TargetNum].position, TargetPos ) < 0.1f)
        {
            TargetNum += 1;
            TargetNum = Mathf.Clamp(TargetNum, 2, Nodes.Length-1);
        }
	}

    private void OnDrawGizmos()
    {
        Transform [] Nodes = transform.GetComponentsInChildren<Transform>();
        for (int i = 3; i < Nodes.Length; i++)
        {
            Gizmos.DrawLine(Nodes[i - 1].position, Nodes[i].position);
            Gizmos.DrawSphere(Nodes[i - 1].position, 0.4f);
        }
    }
}
