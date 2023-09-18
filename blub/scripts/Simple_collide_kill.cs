using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_collide_kill : MonoBehaviour {

    //Rigidbody rb;
    private bool Died;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("head") || col.gameObject.CompareTag("body"))
        {
            if (Died == false)
            {
                Debug.Log("Blub killed by simple kill");
                Died = true;
            }
        }
    }
}
