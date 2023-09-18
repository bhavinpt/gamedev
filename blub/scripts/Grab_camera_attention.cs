using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_camera_attention : MonoBehaviour {

    camera_follow MainCam;
    public Vector3 NewOffset,New_CamRotOffset;
    static bool Another_Attention_Grabbed;

	void Start () {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera_follow>();
        StartCoroutine(Z_calibration());
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("body") || other.gameObject.CompareTag("head") && !Another_Attention_Grabbed)
        {
            MainCam.Offset = NewOffset;
            MainCam.CamRotOffset = New_CamRotOffset;
            Another_Attention_Grabbed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("body") || other.gameObject.CompareTag("head"))
        {
            MainCam.Offset = MainCam.Original_offset;
            MainCam.CamRotOffset = MainCam.Original_CamRotOffset;
            Another_Attention_Grabbed = false;
        }
    }

    IEnumerator Z_calibration()
    {
        yield return new WaitForSeconds(0.5f);
        NewOffset.z += MainCam.Offset.z;
    }

}
