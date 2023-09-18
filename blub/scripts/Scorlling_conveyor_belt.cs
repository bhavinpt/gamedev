using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorlling_conveyor_belt : MonoBehaviour {


    public float jumpForce, ScrollScale , scrollSpeed , PushForce;
    float Object_ScrollSpeed , _time ;
    Renderer Conv_renderer;
    public Vector2 TextureScale;
    public bool On;

    void Start()
    {
        Conv_renderer = GetComponent<Renderer>();     // belt is child
        float Width = Conv_renderer.bounds.size.x*4f;
        TextureScale.x = Width;
        Conv_renderer.material.mainTextureScale = TextureScale;
        //scrollSpeed *= transform.localScale.x;

    }

    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed*ScrollScale, 0);
        Conv_renderer.material.mainTextureOffset = textureOffset;

    }
    
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody ColRb = collision.transform.GetComponent<Rigidbody>();

        if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("body"))
        {
            //ColRb.AddForce(transform.right * scrollSpeed);
            ColRb.MovePosition(ColRb.transform.position + (transform.right * scrollSpeed * Time.deltaTime));
        }

        else
        {
            //ColRb.MovePosition(ColRb.transform.position + (transform.right * scrollSpeed * Time.deltaTime));


            Vector3 _deltaForce = new Vector3(ColRb.mass *scrollSpeed / (Time.deltaTime*10f), 0, 0);
            ColRb.AddForce(_deltaForce, ForceMode.Force);

        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("head") || other.gameObject.CompareTag("body"))
        {
            //Debug.Log(Time.time - _time);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("body"))// || collision.gameObject.CompareTag("head"))
        {
            //////  Adds force after jumping off the belt   ////////
            //StartCoroutine(Jump(collision.gameObject.GetComponent<Rigidbody>()));
            _time = Time.time;
        }

    }

    IEnumerator Jump(Rigidbody Rb)
    {
        On = true;
        StartCoroutine(JumpOff());
        while (On)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Rb.AddForce(new Vector3(scrollSpeed * jumpForce, 0, 0) , ForceMode.Acceleration);
        }
    }

    IEnumerator JumpOff()
    {
        yield return new WaitForSeconds(1.1f);
        On = false;
    }


}
