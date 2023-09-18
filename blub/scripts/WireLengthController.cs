using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLengthController : MonoBehaviour {

    GameObject HeadJoint, SingleWire , RecentWire;
    Transform Chainpoints;
    Rigidbody BodyRb;
    ConfigurableJoint BodySpring;
    bool MakeWire , RollWire;
    private bool InitialWireMade;
    float ChainWidth  ,ChainLength;
    public float MaxChainLength , RollingfForce;

    List<Rigidbody> Segments = new List<Rigidbody>();

    void Start () {
        HeadJoint = transform.GetChild(0).GetChild(0).gameObject;
        BodyRb = transform.GetChild(1).GetComponent<Rigidbody>();
        Chainpoints = transform.GetChild(2);
        SingleWire = Resources.Load("wire_segment") as GameObject;
        BodySpring = transform.GetChild(1).GetComponent<ConfigurableJoint>();
        //ChainWidth = SingleWire.GetComponent<Renderer>().bounds.size.y;
        ChainWidth = 0.3f;
    }

	
	void Update () {
		if(Input.GetKeyDown(KeyCode.J) && !MakeWire)
        {
            MakeWire = true;
        }
        if (InitialWireMade && Input.GetKey(KeyCode.R) )
        {
            RollWire = true;
        }
        else
        {
            RollWire = false;
        }

	}

    private void FixedUpdate()
    {
        
        if (InitialWireMade && (ChainLength < MaxChainLength) && (Vector3.Distance(RecentWire.transform.position, BodyRb.position) > ChainWidth))
        {
            Debug.Log(Vector3.Distance(RecentWire.transform.position, BodyRb.position) + "  ch wdth"+ChainWidth);

            GameObject NewWire = Instantiate(SingleWire, RecentWire.transform.GetChild(0).position, Quaternion.identity, Chainpoints) as GameObject;
            ConfigurableJoint NewWireJoint = NewWire.GetComponent<ConfigurableJoint>();       // get new wire joint

            NewWireJoint.connectedBody = RecentWire.GetComponent<Rigidbody>();          // connect new wire to recent wire
            BodySpring.connectedMassScale = 1 / RecentWire.GetComponent<Rigidbody>().mass;

            Physics.IgnoreCollision(NewWire.GetComponent<SphereCollider>(), RecentWire.GetComponent<SphereCollider>());
            BodySpring.connectedBody = NewWire.GetComponent<Rigidbody>();               // body connected with new wire
            BodySpring.autoConfigureConnectedAnchor = false;                            // set body connected anchor on new wire
            BodySpring.connectedAnchor = Vector3.zero;
            RecentWire = NewWire;                                                       // make new wire recent
            Segments.Add(RecentWire.GetComponent<Rigidbody>());
            ChainLength += 1;
        }

        else if (RollWire)
        {
            Segments[Segments.Count - 1].AddForce((BodyRb.position - Segments[Segments.Count - 1].position) * RollingfForce);

            Debug.Log(Vector3.Distance(BodyRb.position, Segments[Segments.Count - 1].position));
            if (Vector3.Distance(BodyRb.position, Segments[Segments.Count-1].position) < 0.3f)
            {
                BodySpring.connectedBody = Segments[Segments.Count - 2];
                BodySpring.autoConfigureConnectedAnchor = false;
                BodySpring.connectedAnchor = Vector3.zero;

                //RecentWire=TempRecentwire.GetComponent<ConfigurableJoint>()
                Segments.RemoveAt(Segments.Count - 1);
                Destroy(Segments[Segments.Count - 1].gameObject);


            }

        }
        if (MakeWire)
        {
            MakeWire = false;
            RecentWire = Instantiate(SingleWire, BodyRb.transform.position, Quaternion.identity, Chainpoints) as GameObject;
            ConfigurableJoint RecentWireJoint = RecentWire.GetComponent<ConfigurableJoint>();         // first wire

            RecentWireJoint.connectedBody = transform.GetChild(0).GetComponent<Rigidbody>();    // connected with head
            RecentWireJoint.connectedAnchor = new Vector3(0, -0.2f, 0);
            RecentWireJoint.connectedMassScale = transform.GetChild(0).GetComponent<Rigidbody>().mass*2;

            BodySpring.connectedBody = RecentWire.GetComponent<Rigidbody>();                // body connected with first wire
            BodySpring.autoConfigureConnectedAnchor = false;
            BodySpring.connectedAnchor = Vector3.zero;
            //BodySpring.connectedMassScale = 1 / BodySpring.GetComponent<Rigidbody>().mass;
            Segments.Add(RecentWire.GetComponent<Rigidbody>());
            InitialWireMade = true;
            Debug.Log("initial wire made");
        }

    }
}
