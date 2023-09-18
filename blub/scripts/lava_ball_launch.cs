using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava_ball_launch : MonoBehaviour {

    GameObject LavaBallPrefab;
    private bool launched=true;
    public float LaunchRate;

    void Start () {
        LavaBallPrefab = Resources.Load("lava_ball") as GameObject;
    }

    private void Update()
    {
        if (launched)
        {
            StartCoroutine(LaunchLavaBall());
            launched = false;
        }
    }

    IEnumerator LaunchLavaBall()
    {
        yield return new WaitForSeconds(LaunchRate);
        Instantiate(LavaBallPrefab, transform.position, Quaternion.identity);
        launched = true;
    }

   
}
