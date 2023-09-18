using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_simple : MonoBehaviour {

    public Rigidbody Ghost;
    public float Follow_speed;
    private ParticleSystem Ghost_Particle;

    private bool Following;
    private Transform Target , SecondTarget;

    private void Start()
    {
        Ghost_Particle = Ghost.GetComponent<ParticleSystem>() ;
        Ghost_Particle.Play(true);
        Ghost.position = transform.position;
    }

    private void Update()
    {
        if (Following)
        {
            Ghost.MovePosition( Vector3.MoveTowards(Ghost.position, Target.position, Time.deltaTime * Follow_speed));
            //Ghost.velocity = (Target.position - Ghost.position) * Follow_speed;
        }
        else
        {
            Ghost.MovePosition( Vector3.MoveTowards(Ghost.position, transform.position, Time.deltaTime * Follow_speed));
            //Ghost.velocity = (transform.position - Ghost.position) * Follow_speed;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            if ( ! Following)
            {
                Target = other.transform;
                Following = true;
            }
            else
            {
                SecondTarget = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            if (other.transform == Target)
            {
                if (SecondTarget != null)
                {
                    Target = SecondTarget;
                    SecondTarget = null;
                }
                else
                {
                    Debug.Log("primary exit");
                    Following = false;
                }
            }
            else if (other.transform == SecondTarget)
            {
                SecondTarget = null;
            }
        }
    }


}
