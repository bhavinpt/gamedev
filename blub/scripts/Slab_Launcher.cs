using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slab_Launcher : MonoBehaviour {

    GameObject Slab;
    private bool Thrown =true;

    public Vector3 LaunchForce;
    public float LaunchRate;

    void Start () {
        Slab = Resources.Load("slab_jump") as GameObject;
	}
	
	void Update () {
        if (Thrown)
        {
            StartCoroutine(Launch());
            Thrown = false;
        }
	}

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(LaunchRate);
        GameObject Recent_slab =Instantiate(Slab, transform.position, Quaternion.identity) as GameObject;
        Recent_slab.GetComponent<Rigidbody>().AddForce(LaunchForce,ForceMode.VelocityChange );
        Thrown = true;
    }
    
}
