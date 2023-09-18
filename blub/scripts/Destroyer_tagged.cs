using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_tagged : MonoBehaviour {

    public string Tag_to_destroy;

	// Use this for initialization
	void Start () {
        if (Tag_to_destroy==null)
        {
            Debug.Log("add Destroyer tag");
        }		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(Tag_to_destroy)) 
        {

            Destroy(collision.gameObject);
        }
    }
}
