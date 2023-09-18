using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_kill : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            Debug.Log("kill");
        }
    }
}
