using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge : MonoBehaviour {

    GameObject PlankPrefab,BrokenPlankPrefab;
    GameObject [] Planks;
    public bool Flexible, SingleFall, SeqFall;
    public  int [] BreakIdxFromLastInPercent ;
    public float SequenceFallDelay, InitialDelayeInSequencefall;

    private bool Collapse, SequenceCollapse;

    void Start () {
        Transform Start = transform.GetChild(0);
        Transform End = transform.GetChild(1);

        PlankPrefab = Resources.Load("bridge_plank") as GameObject;
        BrokenPlankPrefab = Resources.Load("broken_bridge_plank") as GameObject;
        Vector3 PlankWidth = PlankPrefab.GetComponent<Renderer>().bounds.size;

        float BridgeLength = Vector3.Distance(Start.position, End.position);
        int TotalPlanks = (int)(BridgeLength / PlankWidth.x) + 1;

        if (Start.position.x > End.position.x)
        {
            PlankWidth.x = -PlankWidth.x;
            Vector3 Startpos = Start.position;
            Startpos.x += PlankWidth.x / 2;
            Start.position = Startpos;
        }

        Planks = new GameObject [TotalPlanks];

        for(int i = 0; i < TotalPlanks; i++)
        {
            Vector3 OffsetPos = new Vector3(PlankWidth.x*i, 0, 0) + Start.position;

            Planks[i] = Instantiate(PlankPrefab, OffsetPos, Quaternion.identity) as GameObject;
        }
        /////////    for flexible bridge    /////////
        if (Flexible)
        {
            HingeJoint B_Joint;
            for (int i = 0; i < TotalPlanks; i++)
            {
                
                if (i == 0)     ////////    connect first plank with start point    //////
                {
                    B_Joint = Planks[i].AddComponent<HingeJoint>();
                    //B_Joint.connectedBody = Start.GetComponent<Rigidbody>();
                    B_Joint.anchor = Vector3.zero;
                    B_Joint.autoConfigureConnectedAnchor = true;
                    B_Joint.axis = Vector3.forward;
                }
                else    ////////    successive connections  /////////
                {
                    B_Joint = Planks[i].AddComponent<HingeJoint>();
                    B_Joint.connectedBody = Planks[i - 1].GetComponent<Rigidbody>();
                    B_Joint.anchor = Vector3.zero;
                    B_Joint.autoConfigureConnectedAnchor = false;
                    B_Joint.axis = Vector3.forward;
                }
            }
            /*B_Joint=Planks[TotalPlanks - 1].AddComponent<HingeJoint>();
            B_Joint.anchor = new Vector3(0,0,0);
            B_Joint.autoConfigureConnectedAnchor = false;
            B_Joint.axis = Vector3.up;
            */
            B_Joint = End.GetComponent<HingeJoint>();
            B_Joint.connectedBody= Planks[TotalPlanks - 1].GetComponent<Rigidbody>();
        }

        //////  else make bridge solid  ///////
        else
        {
            for (int i = 0; i < TotalPlanks; i++)
            {
                Planks[i].GetComponent<Rigidbody>().isKinematic = true;

            }
        }
        for (int i = 0; i < BreakIdxFromLastInPercent.Length; i++)
        {
            BreakIdxFromLastInPercent[i] = (int)TotalPlanks * BreakIdxFromLastInPercent[i] / 100;
        }
        
    }
    private void Update()
    {
        if (SequenceCollapse)
        {
            StartCoroutine(SequenceFall());
            SequenceCollapse = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (Collapse == false && col.CompareTag("bulb"))
        {
            if (SingleFall)
            {
                for (int i = 0; i < BreakIdxFromLastInPercent.Length; i++)
                {


                    Collapse = true;
                    Vector3 BreakPlankPos = Planks[BreakIdxFromLastInPercent[i]].transform.position;
                    Quaternion BreakPlankRot = Planks[BreakIdxFromLastInPercent[i]].transform.rotation;
                    Destroy(Planks[BreakIdxFromLastInPercent[i]]);
                    GameObject BrokenPlank = Instantiate(BrokenPlankPrefab, BreakPlankPos, BreakPlankRot) as GameObject;
                    Planks[BreakIdxFromLastInPercent[i]] = BrokenPlank;
                    BrokenPlank.transform.GetChild(0).SetParent(null);
                    Rigidbody BrokenPlankRb = BrokenPlank.GetComponent<Rigidbody>();

                    if (Flexible)
                    {
                        HingeJoint Joint = BrokenPlank.AddComponent<HingeJoint>();
                        Joint.connectedBody = Planks[BreakIdxFromLastInPercent[i] - 1].GetComponent<Rigidbody>();
                        Joint.anchor = Vector3.zero;
                        Joint.autoConfigureConnectedAnchor = false;
                        Joint.axis = Vector3.up;
                        Planks[BreakIdxFromLastInPercent[i] + 1].GetComponent<HingeJoint>().connectedBody = BrokenPlank.GetComponent<Rigidbody>();
                    }
                    else
                    {
                        BrokenPlankRb.isKinematic = true;
                    }
                }
            }

            if(SeqFall)
            {
                StartCoroutine(DelayInSequenceFall());
            }
        }
    }

        IEnumerator SequenceFall()
        {
            for (int i = 1; i < Planks.Length-1; i++)
            {
                bool Match = false;

                if (Planks[i] != null)
                {
                        for (int j = 0; j < BreakIdxFromLastInPercent.Length; j++)
                        {
                            if (i==BreakIdxFromLastInPercent[j])
                            {
                                Debug.Log("match");
                                Match = true;
                                break;
                            }
                        }
                    if (!Match)
                    {
                        yield return new WaitForSeconds(SequenceFallDelay);
                        Vector3 BreakPlankPos = Planks[i].transform.position;
                        Quaternion BreakPlankRot = Planks[i].transform.rotation;
                        Destroy(Planks[i]);
                        GameObject BrokenPlank = Instantiate(BrokenPlankPrefab, BreakPlankPos, BreakPlankRot) as GameObject;
                        Planks[i] = BrokenPlank;
                        BrokenPlank.transform.GetChild(0).SetParent(null);
                        Rigidbody BrokenPlankRb = BrokenPlank.GetComponent<Rigidbody>();

                        if (Flexible)
                        {
                            HingeJoint Joint = BrokenPlank.AddComponent<HingeJoint>();
                            Joint.connectedBody = Planks[i - 1].GetComponent<Rigidbody>();
                            Joint.anchor = Vector3.zero;
                            Joint.autoConfigureConnectedAnchor = false;
                            Joint.axis = Vector3.up;
                            Planks[i + 1].GetComponent<HingeJoint>().connectedBody = BrokenPlank.GetComponent<Rigidbody>();
                        }
                        else
                        {
                            BrokenPlankRb.isKinematic = true;
                        }

                    }
                }
            }
            SequenceCollapse = false;

        }

        IEnumerator DelayInSequenceFall()
        {
            yield return new WaitForSeconds(InitialDelayeInSequencefall);
            SequenceCollapse = true;
        }
}
