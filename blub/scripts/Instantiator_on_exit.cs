using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator_on_exit : MonoBehaviour {

    public GameObject Prefab_to_instantiate;
    public string Exit_tag;

	// Use this for initialization
	void Start () {
        if (Prefab_to_instantiate==null)
        {
            Debug.Log("prefab not assigned in on_exit instantiator");
        }
        if (Exit_tag==null)
        {
            Debug.Log("Tag not assigned in on_exit instantiator");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Exit_tag))
        {
            Instantiate(Prefab_to_instantiate, transform.position, Quaternion.identity, transform);
        }
    }
}
