using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb_movement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed , jumpForce, MaxSpeed ;
    public float DistanceToGround , WallJumpDist , WallJumpForce;
    public Vector3 RayOffset;
    private bool InTheAir, RightMove , LeftMove , Jump , InputDisabled , DoubleJumpValid;
    RaycastHit HitInfo;

    void Awake()
    {
        rb = transform.GetChild(1).GetComponent<Rigidbody>();
        DoubleJumpValid = true;
    }

    private void Update()
    {
        if (!InputDisabled)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                RightMove = true;
            }
            else
            {
                RightMove = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                LeftMove = true;
            }
            else
            {
                LeftMove = false;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }
    }

    void FixedUpdate()
    {
        if (!rb.isKinematic && rb.useGravity)
        {
            //rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.identity, Time.deltaTime * 120f);
            if (Jump)
            {
                //////////////      first jump starts here  /////////////////

                if (IsGrounded() && DoubleJumpValid)
                {
                    if (RightMove)
                    {
                        rb.velocity = new Vector3(rb.velocity.x-1, jumpForce, rb.velocity.z);
                        //rb.AddForce(new Vector3(-1, 1, 0) * jumpForce, ForceMode.VelocityChange);
                    }
                    else if (LeftMove)
                    {
                        rb.velocity = new Vector3(rb.velocity.x+1, jumpForce, rb.velocity.z);
                        //rb.AddForce(new Vector3(1, 1, 0) * jumpForce, ForceMode.VelocityChange);
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                        //rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                    }
                    //if (DoubleJumpValid)    // disable double jump after single jump
                    {
                        StartCoroutine(DoubleJump_DisabledForDuration());
                        DoubleJumpValid = false;
                    }
                    
                }
                    
                //////////////////    second jump starts here   ////////////////////

                else if (Physics.Raycast(rb.position, Vector3.right, WallJumpDist) && DoubleJumpValid)
                {
                    //rb.velocity = new Vector3(-10f, 1, 0) * WallJumpForce;
                    rb.AddForce(new Vector3(-2 , 1, 0) * WallJumpForce, ForceMode.VelocityChange);
                    StartCoroutine(DisableInputForDuration());
                    StartCoroutine(DoubleJump_DisabledForDuration());
                                Debug.DrawRay(rb.position, Vector3.right * WallJumpDist, Color.green);
                    DoubleJumpValid = false;
                }

                else if (Physics.Raycast(rb.position, Vector3.left, WallJumpDist) && DoubleJumpValid)
                {
                    //rb.velocity = new Vector3(10f, 1, 0) * WallJumpForce;
                    rb.AddForce(new Vector3(2, 1, 0) * WallJumpForce, ForceMode.VelocityChange);
                    StartCoroutine(DisableInputForDuration());
                    StartCoroutine(DoubleJump_DisabledForDuration());
                                Debug.DrawRay(rb.position, Vector3.left * WallJumpDist, Color.green);
                    DoubleJumpValid = false;
                }
            }

            // *** TO-DO *** Add support for move after jump in below

            Vector3 ForwardToNormal;

             if (RightMove && !InputDisabled)
            {
                Physics.Raycast(rb.position, -Vector3.up, out HitInfo, DistanceToGround);
                ForwardToNormal = Vector3.Cross(transform.forward, HitInfo.normal);
                rb.AddForce(ForwardToNormal * speed, ForceMode.VelocityChange);
                        Debug.DrawRay(rb.position, ForwardToNormal * WallJumpDist, Color.magenta);
            }
            else if (LeftMove && !InputDisabled)
            {
                Physics.Raycast(rb.position, -Vector3.up, out HitInfo, DistanceToGround);
                ForwardToNormal = Vector3.Cross(-transform.forward, HitInfo.normal);
                rb.AddForce(ForwardToNormal * speed, ForceMode.VelocityChange);

                            Debug.DrawRay(rb.position, ForwardToNormal * WallJumpDist, Color.magenta);
            }

            Vector3 vel = rb.velocity;
            
                vel.x = Mathf.Clamp(vel.x, -MaxSpeed, MaxSpeed);
                vel.z = Mathf.Clamp(vel.z, -MaxSpeed, MaxSpeed);
                //vel.y = Mathf.Clamp(vel.y, -MaxSpeed, MaxSpeed);

            rb.velocity = vel;

            DrawDebugLines();
        }
    }
    bool IsGrounded()
    {
        if (Physics.Raycast(rb.position, -Vector3.up, DistanceToGround) ||
            Physics.Raycast(rb.position + RayOffset, -Vector3.up, DistanceToGround) ||
            Physics.Raycast(rb.position - RayOffset, -Vector3.up, DistanceToGround) ) 
        {
            Debug.DrawRay(rb.position, -Vector3.up*DistanceToGround , Color.cyan);
            //InTheAir = true;
            return true;
        }
        else
        {
            Debug.DrawRay(rb.position, -Vector3.up * DistanceToGround, Color.yellow);
            //InTheAir = false;
            return false;
        }
    }

    IEnumerator DisableInputForDuration()
    {
       // Debug.Log("input disabled ");
        InputDisabled = true;

        Jump = RightMove = LeftMove = false;

        yield return new WaitForSeconds(0.7f);
        InputDisabled = false;
    }

    IEnumerator DoubleJump_DisabledForDuration()
    {
       // Debug.Log("double jump disabled ");
        
        yield return new WaitForSeconds(0.3f);
        DoubleJumpValid = true;
    }

    void DrawDebugLines()
    {
        Debug.DrawRay(rb.position, Vector3.right * WallJumpDist, Color.green);
        Debug.DrawRay(rb.position, -Vector3.up * DistanceToGround, Color.cyan);
        Debug.DrawRay(rb.position + RayOffset, -Vector3.up * DistanceToGround, Color.cyan);
        Debug.DrawRay(rb.position - RayOffset, -Vector3.up * DistanceToGround, Color.cyan);
    }
}



