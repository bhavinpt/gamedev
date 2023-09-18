using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_X_axis : MonoBehaviour {

    public float TimeToRelease,     //time for which it stays pressed
                 TimeToHit,         //time for which it stays ready to press
                 HitForce,          // speed of hitting
                 StartDelay;        // initial delay to start pressing (useful woth multiple pressing object)
    public Vector3 Endpos;          // how far object travels
    public bool flip;

    Rigidbody Rb;
    bool hit;
    float CurrentTime;
    Vector3 StartPos;


    ///// object will start with pressing down After initial delay
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        if (TimeToRelease == 0)
        {
            TimeToRelease = TimeToHit / 3;
        }

        if (flip)
        {
            HitForce = -HitForce;               // switch initial direction and target
            Endpos.x = -Endpos.x;

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
        CurrentTime = TimeToRelease;
        StartCoroutine(InitialDelay());
    }

    void FixedUpdate()
    {
        if (hit)
        {
            Rb.velocity = Vector3.left * HitForce;
            hit = false;
            HitForce = -HitForce;
            CurrentTime = (TimeToHit + TimeToRelease) - CurrentTime;        // toggling , starting with toggling time to hit

            StartCoroutine(HammerWait());
        }
        if (Rb.position.x > StartPos.x)        // applies min limit
        {
            Rb.position = StartPos;
            Rb.velocity = Vector3.zero;
        }
        if (Rb.position.x < Endpos.x)       // applies max limit
        {
            Rb.position = Endpos;
            Rb.velocity = Vector3.zero;
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

