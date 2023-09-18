using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belt_slide : MonoBehaviour
{
    public Vector3 BeltSpeed;
    public float SpeedScaleFactor;
    bool Touching;
    Rigidbody ColRb;

    void Start()
    {
        BeltSpeed.x = GetComponentInChildren<ConveyerBelt>().BeltSpeed * SpeedScaleFactor;
    }

    private void OnCollisionStay(Collision collision)
    {
        ColRb = collision.transform.GetComponent<Rigidbody>();
        if (ColRb != null)
        {
            if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("body"))
            {
                ColRb.position -= transform.right * BeltSpeed.x * Time.deltaTime;
                ColRb.MovePosition(ColRb.position + transform.right * BeltSpeed.x * Time.deltaTime);
                //ColRb.AddForce(transform.right * BeltSpeed.x, ForceMode.VelocityChange);
            }
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("head"))
        {
            ColRb = collision.transform.GetComponent<Rigidbody>();
            Touching = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("head"))
        {
            Touching = false;
        }

    }*/
}