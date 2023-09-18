using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulling_box : MonoBehaviour {

    //////      Keep box mass to 1 for proper results       /////

    //  **** TO-DO ****  Find optimal setting for physics material of box ... to avoid heavy friction with land without being too slippery

    Rigidbody ParentBoxRb;
    public float ForceFactor;

    private void Start()
    {
        ParentBoxRb = transform.parent.GetComponent<Rigidbody>();
        ForceFactor = ParentBoxRb.mass * ForceFactor;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("body"))
        {
            //Debug.Log(collision.gameObject);
            if (Input.GetKey(KeyCode.P))
            {
                ParentBoxRb.AddForce((collision.transform.position - ParentBoxRb.position) * ForceFactor);
            }
        }
        else if (collision.gameObject.CompareTag("head"))
        {

        }
    }

}
