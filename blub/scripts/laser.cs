using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {

    private LineRenderer Lase;
    private bool Flame_on ;
    public Transform Laser_target;
    public float Distance;
    public bool OnByDefault;

    public ParticleSystem [] particles;

    void Start () {
        Lase = GetComponent<LineRenderer>();
        StartCoroutine(Init_flames());
    }
	
	void Update () {
        RaycastHit hit;
        Debug.DrawRay(transform.position , transform.right * Distance, Color.cyan);

        if (Physics.Raycast(transform.position, transform.right, out hit, Distance))
        {
            Lase.SetPosition(0, transform.position);
            Lase.SetPosition(1, hit.point);

            if (Flame_on == OnByDefault && !hit.transform.CompareTag("laser_target"))   // laser interrupted
            {
                Flip_Flames();
            }
            else if (!Flame_on == OnByDefault && hit.transform.CompareTag("laser_target"))   // laser returned 
            {
                Flip_Flames();
            }
        }
	}

    private void Flip_Flames()
    {
        foreach (ParticleSystem flame in particles)
        {
            if (flame.isEmitting)
            {
                flame.Stop(true);
                Flame_on = false;
            }
            else
            {
                flame.Play(true);
                Flame_on = true;
            }
        }
    }

    IEnumerator Init_flames()
    {
        yield return new WaitForSeconds(1f);

        {
            if (!OnByDefault)
            {
                Debug.Log("resetting");
                foreach (ParticleSystem flame in particles)
                {
                    flame.Stop(true);
                    Flame_on = false;
                }
            }
            else
            {
                foreach (ParticleSystem flame in particles)
                {
                    flame.Play(true);
                    Flame_on = true;
                }
            }
        }
    }

}
