using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildOnTrigger : MonoBehaviour {
    private void Awake()
    {
        if (transform.childCount != 0) 
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            this.enabled = false;
            //Destroy(this.gameObject);
        }
    }
}
