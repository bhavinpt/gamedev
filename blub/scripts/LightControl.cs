using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {
    public float DischargeSpeed;
    private float MaxCharge;
    Light BulbLight;
	
	void Start ()
    {
        BulbLight = GetComponent<Light>();
        MaxCharge = BulbLight.intensity;
	}
	
	void Update ()
    {
        BulbLight.intensity -= Time.deltaTime * DischargeSpeed;
        BulbLight.intensity = Mathf.Clamp(BulbLight.intensity, 0f, MaxCharge);
        if (BulbLight.intensity <= 0)
        {
            Debug.Log("@@@@  Lights off  ! @@@@");
        }
	}
}
