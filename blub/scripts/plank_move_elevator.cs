using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plank_move_elevator : MonoBehaviour {

    Transform Target;
    Rigidbody rb;
    float MoveSpeed=2f;

	void Start () {
        rb = GetComponent<Rigidbody>();
        Target = transform.parent.GetChild(0);
        //StartCoroutine(WaitForParent());
	}
	
	void FixedUpdate () {
        Vector3 Target_InterPos = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * MoveSpeed);
        rb.MovePosition(Target_InterPos);
        if (transform.position == Target.position)
        {
            Destroy(gameObject);
        }
	}

    IEnumerator WaitForParent()
    {
        yield return new WaitForSeconds(0.1f);
    }


}
