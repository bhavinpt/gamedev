using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {

    public GameObject Prefab;
    public float LaunchDuration;
    private bool _CanLaunch;

    private void Start()
    {
        if (Prefab==null)
        {
            Debug.Log("failed to put prefab in instantiator");
        }
        else
        {
            _CanLaunch = true;
        }
        
    }

    void Update () {
        if (_CanLaunch)
        {
            StartCoroutine(WaitForLaunch());
            Instantiate(Prefab, transform.position, Quaternion.identity);
            _CanLaunch = false;
        }
	}

    IEnumerator WaitForLaunch()
    {
        yield return new WaitForSeconds(LaunchDuration);
        _CanLaunch = true;
    }
}
