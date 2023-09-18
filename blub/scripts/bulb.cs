using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulb : MonoBehaviour {
	Rigidbody rb;


    public float speed;
	public float jumpForce , MaxSpeed;

    bool Jumping;
    float DistanceToGround;
    private bool RightMove;
    private bool LeftMove;
    private bool Jump;
    
    ParticleSystem bubble,Shock;
    Light ShockLight;

    // private void Awake()
    // {
    //     foreach (Transform item in transform)
    //     {
    //         if (item.CompareTag("bubble"))
    //         {
    //             bubble = item.GetComponent<ParticleSystem>();
    //         }
    //         if (item.CompareTag("shock"))
    //         {
    //             Shock = item.GetComponent<ParticleSystem>();
    //             ShockLight = item.GetChild(0).GetComponent<Light>();
    //             ShockLight.enabled = false;
    //         }
    //     }
    //     bubble.Stop();
    //     Shock.Stop();
    // }

    void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
        DistanceToGround = GetComponent<Collider>().bounds.extents.y;
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {          
            Jump = true;
        }
        else
        {
            Jump = false;
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            RightMove = true;
        }
        else{
            RightMove = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow)){
            LeftMove = true;
        }
        else
        {
            LeftMove = false;
        }
    }

        void FixedUpdate () {
        if (rb.useGravity)
        {
            rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.identity, Time.deltaTime * 120f);

            if (IsGrounded() && Jump)
            {
                if (RightMove)
                {
                    rb.AddForce(new Vector3(-1, 1, 0) * jumpForce, ForceMode.VelocityChange);
                }
                else if (LeftMove)
                {
                    rb.AddForce(new Vector3(1,1,0) * jumpForce, ForceMode.VelocityChange);
                }
                else
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                }
            }
            else if (RightMove)
            {
                rb.AddForce(-Vector3.right * speed, ForceMode.VelocityChange);
            }
            else if (LeftMove)
            {
                rb.AddForce(Vector3.right * speed, ForceMode.VelocityChange);
            }

            Vector3 vel = rb.velocity;

            if (!Jumping && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))    //standing still (no inputs)
            {
                vel.x = 0;
                vel.z = 0;
            }
            else
            {
                vel.x = Mathf.Clamp(vel.x, -MaxSpeed, MaxSpeed);
                vel.z = Mathf.Clamp(vel.z, -MaxSpeed, MaxSpeed);
            }
            rb.velocity = vel;


        }
	}
    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up *  (DistanceToGround + 0.1f), Color.blue);

        if( Physics.Raycast(transform.position, -Vector3.up, DistanceToGround + 0.1f))
        {
            Jumping = false;
            return true;
        }
        else
        {
            Jumping = true;
            return false;
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("water"))
        {   
            transform.GetComponentInChildren<Light>().enabled=false;
            bubble.Play();
            StartCoroutine(shock());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water")){
            bubble.Stop();
        }    
    }
    IEnumerator shock()
    {
        Shock.Play();
        ShockLight.enabled = true;
        yield return new WaitForSeconds(2f);
        Shock.Stop();
        ShockLight.enabled = false;
    }

}


