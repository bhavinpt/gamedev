using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {
    GameObject[] chains;
    GameObject NearestChain , NextChain;
    Transform BulbTrans;
    Rigidbody BulbRb;
    private bool Climbing , RopeCaught , NextRopeCaught, Released;
    public float ClimbSpeed , SwingForce;
    private int Chain_idx , MaxChains;
    public bool LedgeClimbing;
    private bool NoMovement, RightForce, LeftForce, MoveUp, MoveDown, Release;

    void Start () {
    
        chains = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            chains[i] = transform.GetChild(i).gameObject;
        }
        BulbTrans = GameObject.FindGameObjectWithTag("bulb").transform;
        BulbRb = BulbTrans.GetComponent<Rigidbody>();
        MaxChains = chains.Length - 1;
    }
    private void Update()
    {
        if (Released)
        {
            BulbTrans.rotation = Quaternion.RotateTowards(BulbTrans.rotation, Quaternion.identity, Time.deltaTime *80f);
            if (BulbTrans.rotation == Quaternion.identity)
            {
                Released = false;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp = true;
        }
        else
        {
            MoveUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown = true;
        }
        else
        {
            MoveDown = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RightForce = true;
        }
        else
        {
            RightForce = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftForce = true;
        }
        else
        {
            LeftForce = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Release = true;
        }
        else
        {
            Release = false;
        }
        if (!(LeftForce & RightForce & Release & MoveUp & MoveDown))
        {
            NoMovement = true;
        }
        else
        {
            NoMovement = false;
        }

    }

    void FixedUpdate () {
        if (Climbing)
        {
            BulbTrans.position = Vector3.MoveTowards(BulbTrans.position, NearestChain.transform.position, Time.deltaTime * ClimbSpeed*4);
            if (BulbTrans.position==NearestChain.transform.position)
            {
                NextChain = NearestChain;
                BulbTrans.SetParent(NearestChain.transform,true);
                RopeCaught = true;
                NextRopeCaught = true;
            }
        }

        if (RopeCaught)     // on nearest chain ; now grab next chain
        {
            Climbing = false;
            if (LeftForce)
            {
                NextChain.GetComponent<Rigidbody>().AddForce(Vector3.right * SwingForce);

            }
            if (RightForce)
            {
                NextChain.GetComponent<Rigidbody>().AddForce(Vector3.right * -SwingForce);
                Debug.Log(chains[MaxChains]);
            }
            if (Release)
            {
                BulbTrans.SetParent(null);
                BulbRb.useGravity = true;
                //Vector3 dir = new Vector3(NextChain.transform.position.x-transform.position.x,NextChain.transform.position.x-transform.position.y-transform.position.z);
                //BulbRb.AddForce( BulbRb.transform.right , ForceMode.VelocityChange);
                Debug.Log(BulbTrans.right);
                BulbRb.velocity = NextChain.GetComponent<Rigidbody>().velocity ;
                //Debug.Log((BulbRb.velocity.x / Mathf.Abs(BulbRb.velocity.x)));
                //BulbRb.velocity = transform.right*40;
                StartCoroutine(WaitAfterRelease());
                Climbing = false;
                RopeCaught = false;
                NextRopeCaught = false;

                BulbTrans.localScale = Vector3.one;
            }
            if (MoveUp)
            {
                Chain_idx -= System.Convert.ToInt32(NextRopeCaught);
                Chain_idx = Mathf.Clamp(Chain_idx, 0, MaxChains);
                NextChain = chains[Chain_idx];
                CatchNextRope();
            }
            if (MoveDown)
            {

                Chain_idx += System.Convert.ToInt32(NextRopeCaught);
                Chain_idx = Mathf.Clamp(Chain_idx, 0, MaxChains);
                NextChain = chains[Chain_idx];
                CatchNextRope();
            }
            if (NoMovement)
            {
                BulbTrans.position = Vector3.MoveTowards(BulbTrans.position, NextChain.transform.position, Time.fixedDeltaTime * ClimbSpeed);
            }
            transform.localRotation = Quaternion.identity;
        }
		
	}

    void CatchNextRope()
    {/*
        Vector3 movePosition = BulbRb.position;

        movePosition = Vector3.MoveTowards(movePosition, NextChain.transform.position, ClimbSpeed * Time.fixedDeltaTime);

        BulbRb.MovePosition(movePosition);
        */
        BulbTrans.position = Vector3.MoveTowards(BulbTrans.position, NextChain.transform.position, Time.fixedDeltaTime * ClimbSpeed);
        if (BulbTrans.position == NextChain.transform.position)
        {
            BulbTrans.SetParent(NextChain.transform,true);
            //BulbTrans.GetComponent<FixedJoint>().connectedBody = NextChain.GetComponent<Rigidbody>();
            NextRopeCaught = true;
        }
        else
        {
            NextRopeCaught = false;
        }
    }
    IEnumerator WaitAfterRelease()
    {   Debug.Log("ttt");
        Released = true;
        yield return new WaitForSeconds(2f);
        RopeClimb.ClimbingStarted = false;
    }

    public void RopeGrabbed()
    {
        float MinDistance,CurrentDistance;
        MinDistance = 100;
        Chain_idx = 0;
        foreach(GameObject chain in chains)
        {
            Chain_idx++;
            CurrentDistance = Vector3.Distance(BulbTrans.transform.position, chain.transform.position);
            if (CurrentDistance < MinDistance)
            {
                MinDistance = CurrentDistance;
                NearestChain = chain;
                Climbing = true;
            }
        }
        Chain_idx = NearestChain.transform.GetSiblingIndex();
        BulbRb.useGravity = false;
        BulbRb.velocity = Vector3.zero;
    }
}
