using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLaser : MonoBehaviour
{

    LineRenderer Laser;
    public float Distance;
    public Transform Hit_Particles;

    void Start()
    {

        Laser = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up * Distance, Color.yellow);

        if (Physics.Raycast(transform.position, -transform.up, out hit, Distance))      // blue axis is laser's directions
        {
            Laser.SetPosition(0, transform.position);
            Laser.SetPosition(1, hit.point);
            Hit_Particles.position = hit.point;

            if (hit.transform.CompareTag("head") || hit.transform.CompareTag("body"))   // laser interrupted
            {
                Debug.Log("Laser kill ");
            }
        }
        else if (Hit_Particles != null)         // did not hit anything
        {
            Vector3 MissedPos = transform.position + (-transform.up * Distance);
            //Debug.Log(MissedPos);
            //Hit_Particles.gameObject.SetActive(false);
             Laser.SetPosition(0, transform.position);
            Laser.SetPosition(1, MissedPos);
            Hit_Particles.position = MissedPos;
        }
    }
}
