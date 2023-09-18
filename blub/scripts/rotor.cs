using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotor : MonoBehaviour {
    Rigidbody rb;
    public float RotationForce;
    private bool Died;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        rb.AddTorque(transform.forward* RotationForce, ForceMode.VelocityChange);

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("head"))
        {
            if (Died == false && RotationForce!=0)
            {
                Debug.Log("Blub killed by rotor");
                Died = true;
            }
        }
    }
}
