using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

    Rigidbody BodyRb , HeadRb;
    Animator BodyAnim , HeadAnim;
    int Walk_Left_HashId = Animator.StringToHash("body_left");
    int Walk_Right_HashId = Animator.StringToHash("body_right");
    int StandHashId = Animator.StringToHash("body_stand");

    int Walk_Left_Head_HashId = Animator.StringToHash("head_left");
    int Walk_Right_Head_HashId = Animator.StringToHash("head_right");
    int Stand_Head_HashId = Animator.StringToHash("head_stand");

    bool Head_Walking, Head_Standing , Body_Walking , Body_standing;

    void Start () {
 
        HeadRb = transform.GetChild(0).GetComponent<Rigidbody>();
        BodyRb = transform.GetChild(1).GetComponent<Rigidbody>();
        HeadAnim = transform.GetChild(0).GetComponent<Animator>();
        BodyAnim = transform.GetChild(1).GetComponent<Animator>();

    }

    void Update () {

        if (BodyRb.velocity.x > 0.6f && !Body_Walking)   // walking left
        {
            //            Debug.Log(BodyRb.velocity);
            BodyAnim.ResetTrigger(StandHashId);
            BodyAnim.SetTrigger(Walk_Left_HashId);

            Body_Walking = true;
            Body_standing = false;
        }

        else if (BodyRb.velocity.x < -0.6f && !Body_Walking)     // walking right
        {
            //           Debug.Log(BodyRb.velocity);
            BodyAnim.ResetTrigger(StandHashId);
            BodyAnim.SetTrigger(Walk_Right_HashId);

            Body_Walking = true;
            Body_standing = false;
        }
        else if (BodyRb.velocity.x>-0.6f && BodyRb.velocity.x < 0.6f && Body_Walking) 
        {
            BodyAnim.ResetTrigger(Walk_Left_HashId);
            BodyAnim.ResetTrigger(Walk_Right_HashId);
            BodyAnim.SetTrigger(StandHashId);

            Body_Walking = false;
            Body_standing = true;
        }
        else
        {

        }
        
        //////////////  body    ////////////
        
        if (HeadRb.velocity.x > 0.6f && !Head_Walking)   // walking left
        {
            //            Debug.Log(BodyRb.velocity);
            HeadAnim.ResetTrigger(Stand_Head_HashId);
            HeadAnim.SetTrigger(Walk_Left_Head_HashId);

            Head_Walking = true;
            Head_Standing = false;
        }

        else if (HeadRb.velocity.x < -0.6f && !Head_Walking)     // walking right
        {
            //           Debug.Log(BodyRb.velocity);
            HeadAnim.ResetTrigger(Stand_Head_HashId);
            HeadAnim.SetTrigger(Walk_Right_Head_HashId);

            Head_Walking = true;
            Head_Standing = false;
        }
        else if (HeadRb.velocity.x > -0.6f && HeadRb.velocity.x < 0.6f && Head_Walking)
        {
            HeadAnim.ResetTrigger(Walk_Left_Head_HashId);
            HeadAnim.ResetTrigger(Walk_Right_Head_HashId);
            HeadAnim.SetTrigger(Stand_Head_HashId);

            Head_Walking = false;
            Head_Standing = true;
        }
        else
        {

        }
    }
}
