using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Climb : MonoBehaviour {

    public Vector3 ClimbSpeed;
    public float JumpForce;

    private bool _BodyIsInside , _HeadIsInside , UpArrow , DownArrow , Space;
    private Rigidbody Head , Body , _ActivePart;
    private Bulb_movement MovementScript;

    // **** TO-DO ****  body is affected by head gravity after getting detached on ladder 
    // ###  WARNING ### Head goes through after head is attached to body outside of ladder and then detached on ladder
	
	void Start () {
        MovementScript = GameObject.FindGameObjectWithTag("fullbody").GetComponent<Bulb_movement>();
        Head = GameObject.FindGameObjectWithTag("head").GetComponent<Rigidbody>();
        Body = GameObject.FindGameObjectWithTag("body").GetComponent<Rigidbody>();
    }

    void Update()
    {      
        //////// if any Part(s) is on ladder
        if (_HeadIsInside || _BodyIsInside)
        {
            ////////    to control only those part(s) which are on ladder
            if (_HeadIsInside && _BodyIsInside)
            {
                _ActivePart = MovementScript.rb;
            }
            else if (_HeadIsInside)
            {
                _ActivePart = Head;
            }
            else
            {
                _ActivePart = Body;
            }

            ////////    Active part jumps off the ladder if space hit
            if (Input.GetKeyDown(KeyCode.Space) && MovementScript.rb == _ActivePart)
            {
                _ActivePart.isKinematic = false;
                Vector3 NewVal = _ActivePart.velocity;
                NewVal.y = JumpForce;
                _ActivePart.velocity = NewVal;
                _ActivePart.useGravity = true;
                if (_ActivePart == Head)
                {
                    _HeadIsInside = false;
                }
                else
                {
                    _BodyIsInside = false;
                }
            }

            ////////    Up - Down movement
            else if (Input.GetKey(KeyCode.UpArrow) && MovementScript.rb == _ActivePart) 
            {
                _ActivePart.velocity = ClimbSpeed;
                //_ActivePart.MovePosition(_ActivePart.position + (ClimbSpeed * Time.deltaTime));
            }

            else if (Input.GetKey(KeyCode.DownArrow) && MovementScript.rb == _ActivePart)
            {
                _ActivePart.velocity = -ClimbSpeed;
                //_ActivePart.MovePosition(_ActivePart.position - (ClimbSpeed * Time.deltaTime));
            }

            ////////    No movement ... so hold Active part(s) still
            else
            {
                if (_HeadIsInside)
                {
                    Head.velocity = Vector3.zero;
                }
                if (_BodyIsInside)
                {
                    Body.velocity = Vector3.zero;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head"))
        {
            if (Head.GetComponent<FixedJoint>())    // if head is attached
            {
                Head.isKinematic = false;
                _HeadIsInside = false;
            }
            else
            {
                Head.useGravity = false;
                Vector3 _setPos = Head.position;
                _setPos.x = transform.position.x;
                Head.position = _setPos;
                _HeadIsInside = true;
            }
        }
        else if (other.CompareTag("body"))
        {
            if (Head.GetComponent<FixedJoint>())
            {
                Head.isKinematic = false;
                Head.useGravity = false;
                _HeadIsInside = false;
            }
            Body.useGravity = false;
            Vector3 _setPos = Body.position;
            _setPos.x = transform.position.x;
            Body.position = _setPos;
            _BodyIsInside = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("head"))
        {
            Head.isKinematic = false;
            Head.useGravity = true;
            _HeadIsInside = false;
        }

        if (other.CompareTag("body"))
        {
            Body.isKinematic = false;
            Body.useGravity = true;
            _BodyIsInside = false;
        }
    }
}
