using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_stick : MonoBehaviour {
	public float velocity,Angle,MinVelocityToThrow,MaxVelocityToThrow;
	public int resolution;
    Light BulbLight;

	float X_vel_init , Y_vel_init , g;
	LineRenderer line ;
	public Vector2 startPos,endPos;
	public bool clickedToDrag;
    GameObject GlowStick;

	void Start () {
		line = gameObject.GetComponent <LineRenderer> ();
        BulbLight = transform.GetComponentInChildren<Light>();
		g = Physics.gravity.y;
        line.enabled = false;    
    }

    private void Update () { 
	
		if (Input.GetMouseButton (0) && clickedToDrag == true) {        //after first click
			RaycastHit hit2;
			Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition); 
			if (Physics.Raycast (ray2, out hit2)) {
				Vector2 dragpoint = hit2.point;
				velocity = Vector2.Distance (startPos,dragpoint )*4f;

				if (velocity > MinVelocityToThrow) {                                // stretched to release
                    line.enabled = true;
					velocity=Mathf.Clamp (velocity, 0f, MaxVelocityToThrow);

					Angle = Vector2.Angle ( dragpoint - startPos ,dragpoint- new Vector2 (dragpoint.x + 1f, dragpoint.y)  );

					if (dragpoint.y > startPos.y) {
						Angle = 360 - Angle;
						}
					Angle *= Mathf.Deg2Rad;
					X_vel_init = velocity * Mathf.Cos (Angle);
					Y_vel_init = velocity * Mathf.Sin (Angle);

                    line.enabled = true;
                    RenderArc();
				}
                else
                {
                    velocity = 0;
                }
			}
		}
    

		if(Input.GetMouseButtonDown(0) && clickedToDrag==false)		// first click
		{	RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if ( Physics.Raycast (ray,out hit)) {
				if (hit.transform.CompareTag ("bulb")) {
					startPos = (Vector2)hit.point;
					clickedToDrag = true;
                    velocity = 0;
				}
			}
		}

        if (Input.GetMouseButtonUp(0) && clickedToDrag == true)
        {
            if (velocity != 0)
            {
                line.enabled = false;
                GlowStick = Instantiate(Resources.Load("glow_stick"), transform.position, Quaternion.identity) as GameObject;
                GlowStick.GetComponent<Rigidbody>().velocity = new Vector3(X_vel_init, Y_vel_init, 0);
                GlowStick.GetComponent<Rigidbody>().AddTorque(new Vector3(5, 6, 7), ForceMode.VelocityChange);
            }
            else
            {
                BulbLight.enabled = !BulbLight.enabled;
            }
            clickedToDrag = false;

        }
    }

	void RenderArc()
	{	
		line.positionCount =resolution + 1;
		line.SetPositions (CalculateArcArray ());
	}

	Vector3 [] CalculateArcArray()
	{
		Vector3[] arcArray = new Vector3 [resolution + 1];
        float arcLength =( Mathf.Pow(velocity * Mathf.Cos(Angle), 2) * Mathf.Log((1 + Mathf.Sin(Angle)) / (1 - Mathf.Sin(Angle))) ) /20*g;
		line.material.SetTextureOffset( "_MainTex",new Vector2 (arcLength, 1));
		line.material.SetTextureScale( "_MainTex",new Vector2 (arcLength, 1));

		for (int i = 0; i <= resolution; i++) {
			float t = ((float)i / (float)resolution)*Mathf.Sqrt(velocity)/2;
			arcArray [i] = CalculateArcPoint (t);
		}

		return arcArray;
	}

	Vector3 CalculateArcPoint(float t )
	{
		float x = t * X_vel_init;
		float y = (float) Y_vel_init * t +(float) 0.5 * g * t * t;
		return new Vector3 (x, y);
	}
}
