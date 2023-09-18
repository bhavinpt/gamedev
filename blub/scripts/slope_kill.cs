using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slope_kill : MonoBehaviour {
    private bool Triggered;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bulb") && !Triggered)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Triggered = false;
        }
    }
}
