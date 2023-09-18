using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    public float WaitingTimeToRelease,     //time for which it stays pressed
                 WaitingTimeToHit,         //time for which it stays ready to press
                 HitForce,          // speed of hitting
                 StartDelay;        // initial delay to start pressing (useful woth multiple pressing object)
    public Vector3 Endpos;          // how far object travels
    public bool flip;

    Rigidbody Rb;
    bool hit;
    float CurrentTime;
    Vector3 StartPos;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - Endpos, Vector3.one*0.1f);
    }

    ///// object will start with pressing down After initial delay
    void Start () {
        Rb = GetComponent<Rigidbody>();
        if (WaitingTimeToRelease == 0)
        {
            WaitingTimeToRelease = WaitingTimeToHit / 3;
        }

        if (flip)
        {
            HitForce = -HitForce;               // switch initial direction and target
            Endpos.y = -Endpos.y;

            StartPos = Rb.position;             // set start and end
            Endpos = StartPos - Endpos;

            Vector3 TempSwitchPos = Endpos;     //switching start and end
            Endpos = StartPos;
            StartPos = TempSwitchPos;
        }
        else
        {
            StartPos = Rb.position;
            Endpos = StartPos - Endpos;
        }
        CurrentTime = WaitingTimeToRelease;
        StartCoroutine(InitialDelay());
    }

    void Update()
    {
        if (hit)
        {
            Rb.velocity = Vector3.down * HitForce;
            hit = false;
            HitForce = -HitForce;
            CurrentTime = (WaitingTimeToHit + WaitingTimeToRelease) - CurrentTime;        // toggling , starting with toggling time to hit

            StartCoroutine(HammerWait());
        }
        if (Rb.position.y > StartPos.y)        // applies min limit
        {
            Rb.position = StartPos;
            Rb.velocity = Vector3.zero;
        }
        if (Rb.position.y < Endpos.y)       // applies max limit
        {
            Rb.position = Endpos;
            Rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("body") || collision.gameObject.CompareTag("head"))
        {
            Debug.Log("you got stung");
        }
    }

    IEnumerator HammerWait()
    {
        yield return new WaitForSeconds(CurrentTime);
        hit = true;
    }
        IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(StartDelay);
        hit = true;
    }
}
