using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava_ball_destroy : MonoBehaviour {

    public float WaitForDestroy;

	void Start () {
        StartCoroutine(DestroyLavaball());    		
	}
	
    IEnumerator DestroyLavaball()
    {
        yield return new WaitForSeconds(WaitForDestroy);
        Destroy(gameObject);
    }
}
