using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bulb_Joystick : MonoBehaviour
{
    Rigidbody rb;


    public float speed;
    public float jumpForce, MaxSpeed;

    bool Jumping;
    float DistanceToGround;
    private bool RightMove;
    private bool LeftMove;
    private bool Jump;
    public Joystick joystick;

    ParticleSystem bubble, Shock;
    Light ShockLight;
    private GameObject ButtonObject;
    private Button JumpButton;

    private void Start()
    {
        ButtonObject = GameObject.FindGameObjectWithTag("jump_button").gameObject;
        JumpButton = ButtonObject.GetComponent<Button>();
        JumpButton.onClick.AddListener(() => MakeJump());

        foreach (Transform item in transform)
        {
            if (item.CompareTag("bubble"))
            {
                bubble = item.GetComponent<ParticleSystem>();
            }
            if (item.CompareTag("shock"))
            {
                Shock = item.GetComponent<ParticleSystem>();
                ShockLight = item.GetChild(0).GetComponent<Light>();
                ShockLight.enabled = false;
            }
        }
        bubble.Stop();
 
        rb = gameObject.GetComponent<Rigidbody>();
        DistanceToGround = GetComponent<Collider>().bounds.extents.y;

    }
    private void Update()
    {
        Debug.Log(joystick.Horizontal);
        /*if (Input.GetKey(KeyCode.Space))
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }*/

        if (joystick.Horizontal>0)
        {
            RightMove = true;
        }
        else
        {
            RightMove = false;
        }

        if (joystick.Horizontal<0)
        {
            LeftMove = true;
        }
        else
        {
            LeftMove = false;
        }
    }

    void FixedUpdate()
    {
        if (rb.useGravity)
        {
            rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.identity, Time.deltaTime * 120f);
            if (IsGrounded() && Jump)
            {
           
                    rb.AddForce(new Vector3 (joystick.Horizontal,1f,0) * jumpForce, ForceMode.VelocityChange);
              
                Jump = false;
            }
            else if (RightMove)
            {
                rb.AddForce(-Vector3.right*joystick.Horizontal * speed, ForceMode.VelocityChange);
            }
            else if (LeftMove)
            {
                rb.AddForce(Vector3.right *Mathf.Abs(joystick.Horizontal)* speed, ForceMode.VelocityChange);
            }

            Vector3 vel = rb.velocity;

            if (Jumping && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))    //standing still (no inputs)
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
        if (Physics.Raycast(transform.position, -Vector3.up, DistanceToGround + 0.1f))
        {
            Jumping = true;
            return true;
        }
        else
        {
            Jumping = false;
            return false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("water"))
        {
            transform.GetComponentInChildren<Light>().enabled = false;
            bubble.Play();
            StartCoroutine(shock());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
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

    void MakeJump()
    {
        Jump = true;
    }
}


