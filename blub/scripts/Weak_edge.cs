using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weak_edge : MonoBehaviour {

    public float SupportOffset;
    private CapsuleCollider Support;
    private bool Triggered;

	void Start () {
        Support = transform.parent.GetComponent<CapsuleCollider>();
	}
	
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bulb"))
        {
            if (!Triggered)
            {
                Vector3 CenterOfSupport = Support.center;
                CenterOfSupport.y += SupportOffset;
                Support.center = CenterOfSupport;
                Triggered = true;
            }
            else if (Triggered)
            {
                Destroy(Support);
            }
        }
    }

}
