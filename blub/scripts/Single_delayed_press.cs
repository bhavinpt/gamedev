using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_delayed_press : MonoBehaviour {

    public float Init_delay;
    public Vector3 Press_force;
    Rigidbody Rb;
    bool Keep_pressing;

	void Start () {

        Rb = GetComponent<Rigidbody>();

        if (Press_force==Vector3.zero || Rb==null)
        {
            Debug.Log("forgot to apply force to single press or rigidbody"  + "   Force = " + Press_force + "   Rigidbody = "+ Rb);
        }

        Keep_pressing = false;
        StartCoroutine(Initial_delay());
	}
	
	// Update is called once per frame
	void Update () {
        if (Keep_pressing)
        {
            Rb.velocity = Press_force;
        }
    }


    IEnumerator Initial_delay()
    {
        Debug.Log(gameObject.name);
        yield return new WaitForSeconds(Init_delay);
        Keep_pressing = true;
    }
}
