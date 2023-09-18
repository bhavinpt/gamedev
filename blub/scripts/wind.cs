using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour {

    float MaxPos;
    public float WindForce;
    ParticleSystem WindParticle;
    Rigidbody BodyRb, HeadRb;

    void Start() {
        MaxPos = GetComponent<BoxCollider>().bounds.size.x;
        BodyRb = GameObject.FindGameObjectWithTag("body").GetComponent<Rigidbody>();
        HeadRb = GameObject.FindGameObjectWithTag("head").GetComponent<Rigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("head") || other.gameObject.CompareTag("body"))
        {
            Rigidbody Rb = other.GetComponent<Rigidbody>();
            //Debug.Log("blowing");
            float DistanceFromFan = (MaxPos - Vector3.Distance(transform.position, Rb.position) + WindForce);
            DistanceFromFan = Mathf.Clamp(DistanceFromFan, 0, WindForce * 3);

            Rb.AddForce(transform.right * DistanceFromFan * WindForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fanbreaker"))
        {
            StartCoroutine(Stopfan());
        }
    }

    IEnumerator Stopfan()
    {
        yield return new WaitForSeconds(1f);
        WindForce = 0;
        transform.GetComponentInChildren<Rigidbody>().angularDrag = 5f;
        GetComponent<ParticleSystem>().Stop();
        transform.GetComponentInChildren<rotor>().enabled = false;
        Destroy(GetComponent<BoxCollider>());
        this.enabled = false;
    }
}
