using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_controller : MonoBehaviour {

    GameObject Plank;
    private bool Launched;
    public float LaunchRate,MaxLaunchDelayJitter;
	void Start () {
        MaxLaunchDelayJitter = Mathf.Clamp(MaxLaunchDelayJitter, 0.01f, 0.99f);
        MaxLaunchDelayJitter = 1f - MaxLaunchDelayJitter;
        Plank = Resources.Load("elevator_plank")as GameObject;

	}
	
	void Update () {
        if (!Launched)
        {
 
            
            StartCoroutine(PlankLaunch());
            Launched = true;
        }
	}

    IEnumerator PlankLaunch()
    {
        //New_plank.GetComponent<plank_move_elevator>().enabled = true;
        //New_plank.transform.SetParent(transform);
        GameObject New_plank = Instantiate(Plank, transform.position, Quaternion.identity, transform) as GameObject;

        yield return new WaitForSeconds(LaunchRate*Random.Range(MaxLaunchDelayJitter,1f));

        New_plank.AddComponent<plank_move_elevator>();
        Launched = false;
    }

}
