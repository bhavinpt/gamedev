using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_with_ConfigJoint : MonoBehaviour {

    GameObject HeadJoint, SingleWire, RecentWire;
    Transform Chainpoints;
    Rigidbody BodyJointRb;
    ConfigurableJoint BodySpring , RecentWireJoint;
    bool MakeWire;
    private bool InitialWireMade;
    float ChainWidth, ChainLength;
    public float MaxChainLength;

    void Start()
    {
        HeadJoint = transform.GetChild(0).gameObject;
        BodyJointRb = transform.GetChild(1).GetComponent<Rigidbody>();
        Chainpoints = transform.GetChild(2);
        SingleWire = Resources.Load("wire_segment") as GameObject;
        BodySpring = transform.GetChild(1).GetComponent<ConfigurableJoint>();
        //ChainWidth = SingleWire.GetComponent<Renderer>().bounds.size.y;
        ChainWidth = 3f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !MakeWire)
        {
            MakeWire = true;
        }

    }

    private void FixedUpdate()
    {

        if (InitialWireMade && (ChainLength < MaxChainLength) && (Vector3.Distance(RecentWire.transform.position, BodyJointRb.position) > ChainWidth))
        {
            Debug.Log(Vector3.Distance(RecentWire.transform.position, BodyJointRb.position) + "  ch wdth" + ChainWidth);
            GameObject NewWire = Instantiate(SingleWire, RecentWire.transform.GetChild(0).position, Quaternion.identity, Chainpoints) as GameObject;
            ConfigurableJoint NewWireJoint = NewWire.GetComponent<ConfigurableJoint>();       // get new wire joint

            RecentWireJoint.connectedBody = NewWire.GetComponent<Rigidbody>(); // connect previous wire with newest wire
            NewWireJoint.connectedBody = BodyJointRb;          // connect new wire to body

            //RecentWire.connectedBody = NewWire.GetComponent<Rigidbody>();               // connect spring with it
            //BodySpring.autoConfigureConnectedAnchor = false;
            //BodySpring.connectedAnchor = Vector3.zero;
            RecentWire = NewWire;
            RecentWireJoint = NewWireJoint;// make new wire recent
            ChainLength += 1;
        }
        if (MakeWire)
        {
            MakeWire = false;
            RecentWire = Instantiate(SingleWire, BodyJointRb.position, Quaternion.identity, Chainpoints) as GameObject;
            RecentWireJoint = RecentWire.GetComponent<ConfigurableJoint>();

            ConfigurableJoint CfgHeadJoint=HeadJoint.GetComponent<ConfigurableJoint>();     // bulb's cfg joint
            CfgHeadJoint.connectedBody = RecentWire.GetComponent<Rigidbody>();   // connect with first rope

            CfgHeadJoint.autoConfigureConnectedAnchor = false;      
            CfgHeadJoint.connectedAnchor = new Vector3(0, -0.2f, 0);              // set bulb's connected anchor 
            
            RecentWireJoint.connectedBody = BodyJointRb;   // first wire joint connected with body
            //BodySpring.autoConfigureConnectedAnchor = false;
            //BodySpring.connectedAnchor = Vector3.zero;
            InitialWireMade = true;
            Debug.Log("initial wire made");
        }

    }
}

