using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCollision : MonoBehaviour {

    public float MoveSpeed;

    Vector3 [] WayPoints;
    int Pos_count, Current_pos;
    private bool InitiateLaunch;
    Rigidbody ObjectToMove;

    void Start () {
        Transform _PosContainer = transform.GetChild(0);    // The child ; which should have all empties for waypoints as its child
        WayPoints = new Vector3[_PosContainer.childCount];   // array of waypoints
        for (int i = 0; i < _PosContainer.childCount; i++)
        {
            WayPoints[i] = _PosContainer.GetChild(i).transform.position;  // getting waypoints
        }
        ObjectToMove = GetComponent<Rigidbody>();          // the object to be tweened
        ObjectToMove.position = WayPoints[0];            // initialize its positions to starting point
    }
	
	void FixedUpdate () {
        if (InitiateLaunch)
        {
            Vector3 Inter_pos = Vector3.MoveTowards(ObjectToMove.position, WayPoints[Current_pos], Time.deltaTime * MoveSpeed);
            ObjectToMove.MovePosition(Inter_pos);
            if (ObjectToMove.position == WayPoints[Current_pos])
            {
                if (Current_pos + 1 != WayPoints.Length) 
                {
                    Current_pos += 1;
                }

                else
                {
                    this.enabled = false;
                }
            }
        }	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("body"))
        {
            InitiateLaunch = true;
        }
    }
}
