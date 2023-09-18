using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween_looped : MonoBehaviour {

    public float MoveSpeed;

    Vector3[] Pos_arr;
    int Pos_count, Current_pos;
    Transform Moving_Rb;

    void Start () {

        Transform _PosContainer = transform.GetChild(1);    // The 2nd child ; which should have all empties for waypoints as its child
        Pos_count = _PosContainer.childCount;
        Pos_arr = new Vector3 [_PosContainer.childCount];   // array of waypoints
        for (int i = 0; i < _PosContainer.childCount; i++)
        {
            Pos_arr[i] = _PosContainer.GetChild(i).transform.position;  // getting waypoints
        }
        //Moving_Rb = transform.GetChild(0).GetComponent<Rigidbody>();
        Moving_Rb = transform.GetChild(0);          // The 1st child ; which is the object to be tweened
        Moving_Rb.position = Pos_arr[0];            // initialize its positions to starting point
	}
	
	
	void Update ()
    {
        Vector3 Inter_pos = Vector3.MoveTowards(Moving_Rb.position, Pos_arr[Current_pos], Time.deltaTime * MoveSpeed);
        Moving_Rb.position=Inter_pos;
        if (Moving_Rb.position == Pos_arr[Current_pos]) 
        {
            Current_pos = (int)Mathf.Repeat(Current_pos + 1, Pos_count);
        }
		
	}
}
