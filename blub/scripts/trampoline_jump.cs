using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampoline_jump : MonoBehaviour {
    public float Jumpforce;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bulb"))
        {
            Debug.Log("jumpppp" + transform.up);
            collision.gameObject.GetComponent<Rigidbody>().AddForce( transform.up * Jumpforce,ForceMode.VelocityChange);
        }
    }
}
