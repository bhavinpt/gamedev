using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_on_landing : MonoBehaviour {
    Rigidbody Rb;
    public float WaitBeforeFall;

	void Start () {
        Rb = GetComponent<Rigidbody>();
        Rb.isKinematic = true;
        Rb.useGravity = false;
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bulb"))
        {
            StartCoroutine(WaitedFall());
        }
    }

    IEnumerator WaitedFall()
    {
        yield return new WaitForSeconds(WaitBeforeFall);
        Rb.isKinematic = false;
        Rb.useGravity = true;
    }
}
