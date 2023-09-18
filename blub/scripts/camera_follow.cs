using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {
    GameObject bulb;
    public Vector3 Original_offset, Original_CamRotOffset, Offset, CamRotOffset;

    Rigidbody Target;
    public GameObject TargetGameObject;
    public float PositionFollow , RotationFollow;
    Bulb_movement ActivePartScript;

    void Start () {
        //bulb = GameObject.FindGameObjectWithTag ("bulb");        

        ActivePartScript = TargetGameObject.GetComponent<Bulb_movement>();
        Target = ActivePartScript.rb;

            //Offset = transform.position - Target.position;
            Offset.z = transform.position.z-TargetGameObject.transform.position.z;
        
        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
        Original_offset = Offset;
        Original_CamRotOffset = CamRotOffset;

    }
	

	void Update () {


            Target = ActivePartScript.rb;
            transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Time.deltaTime * PositionFollow);

            // Smoothly rotate towards the target point.

            var targetRotation = Quaternion.LookRotation((Target.position + CamRotOffset) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationFollow * Time.deltaTime);
	}
}
