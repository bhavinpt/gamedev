using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_cubes : MonoBehaviour {
    Rigidbody Box_1, Box_2;
    public int init_idx_box1, init_idx_box2, Direction;
    //Transform Top_left, Top_right, Bottom_left, Bottom_right;
    private Vector3[] Points;
    public float RollingSpeed;
    

	void Start () {
        Points = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Points[i] = transform.GetChild(i).position;
        }
        Box_1 = transform.GetChild(4).GetComponent<Rigidbody>();
        Box_2 = transform.GetChild(5).GetComponent<Rigidbody>();
        Box_1.position = Points[init_idx_box1];
        Box_2.position = Points[init_idx_box2];
        init_idx_box1 = (int)Mathf.Repeat(init_idx_box1 + Direction, 4);
        init_idx_box2 = (int)Mathf.Repeat(init_idx_box2 + Direction, 4);
        //init_idx_box1 = Mathf.Abs((init_idx_box1 + Direction) % -4);
        //init_idx_box2 = Mathf.Abs((init_idx_box2 + Direction) % -4);
    }
	
	void FixedUpdate () {
        Vector3 B1 = Vector3.MoveTowards(Box_1.position, Points[init_idx_box1], Time.fixedDeltaTime * RollingSpeed);
        Box_1.MovePosition(B1);
        Vector3 B2 = Vector3.MoveTowards(Box_2.position, Points[init_idx_box2], Time.fixedDeltaTime * RollingSpeed);
        Box_2.MovePosition(B2);
        if (Vector3.Distance(B1, Points[init_idx_box1]) < 0.001f)
        {
            init_idx_box1 = (int)Mathf.Repeat(init_idx_box1 + Direction, 4);
            //init_idx_box1 = Mathf.Abs((init_idx_box1 + Direction) % -4);
        }
        if (Vector3.Distance(B2, Points[init_idx_box2]) < 0.001f)
        {
            init_idx_box2 = (int)Mathf.Repeat(init_idx_box2 + Direction, 4);
            //init_idx_box2 = Mathf.Abs((init_idx_box2 + Direction) % -4);
        }
    }
}
