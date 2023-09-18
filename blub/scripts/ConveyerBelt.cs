using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{

    public int Segments;
    public float BeltSpeed;

    Rigidbody BulbRb, BeltRb;
    GameObject BigSegment, SmallSegment;
    Vector3 StartPos, EndPos;
    float E_pos;
    private bool BulbTouched;

    void Start()
    {
        //BulbRb = GameObject.FindGameObjectWithTag("bulb").GetComponent<Rigidbody>();
        BeltRb = GetComponent<Rigidbody>();
        StartPos = transform.position;
        GameObject belt;
        BigSegment = Resources.Load("big") as GameObject;
        SmallSegment = Resources.Load("small") as GameObject;

        Vector3 SmallSegmentWidth = SmallSegment.GetComponent<Renderer>().bounds.extents;
        Vector3 BigSegmentWidth = BigSegment.GetComponent<Renderer>().bounds.extents;


        Vector3 SegmentSpacing = new Vector3(SmallSegmentWidth.x + BigSegmentWidth.x, 0, 0);
        EndPos = transform.position + SegmentSpacing * 2;
        E_pos = transform.localPosition.x + SegmentSpacing.x * 2;

        for (int i = 0; i < Segments; i++)
        {
            if (i % 2 == 0) // big segment time
            {
                belt = Instantiate(BigSegment, (transform.position - (SegmentSpacing * i)), Quaternion.identity);
            }
            else   // small segment time
            {
                belt = Instantiate(SmallSegment, transform.position - (SegmentSpacing * i), Quaternion.identity);
            }

            belt.transform.SetParent(transform);
        }

        Vector3 colsize = new Vector3(Segments * SegmentSpacing.x, BigSegmentWidth.y * 2, BigSegmentWidth.z * 2);
        BoxCollider col = transform.parent.GetComponent<BoxCollider>();
        col.size = colsize;
        col.center = new Vector3((-Segments * SegmentSpacing.x / 2) + BigSegmentWidth.x, 0, 0);
        transform.localRotation = transform.parent.rotation;
    }

    void Update()
    {
        Vector3 LocalPos = transform.localPosition;
        LocalPos.x += BeltSpeed * Time.deltaTime;
        transform.localPosition = LocalPos;
        //BeltRb.MovePosition(transform.position + transform.TransformDirection(Vector3.right) * BeltSpeed * Time.deltaTime);
        if (transform.localPosition.x > E_pos)
        {
            transform.position = StartPos;
        }
    }
}


