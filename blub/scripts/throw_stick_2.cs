using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_stick_2 : MonoBehaviour {
	public float velocity,Angle;
	public int resolution;

	float radianAngle,g;
	LineRenderer line ;

	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent <LineRenderer> ();
		g = Mathf.Abs (Physics.gravity.y);
	}

	// Update is called once per frame
	void Update () {
		renderArc ();

	}

	void renderArc()
	{	
		line.positionCount =resolution + 1;
		line.SetPositions (calculateArcArray ());
	}

	Vector3 [] calculateArcArray()
	{
		Vector3[] arcArray = new Vector3 [resolution + 1];

		radianAngle = Mathf.Deg2Rad * Angle;
		float maxDistance = (velocity*velocity*Mathf.Sin(2*radianAngle))/g;
		line.material.SetTextureOffset( "_MainTex",new Vector2 (maxDistance, 1));
		line.material.SetTextureScale( "_MainTex",new Vector2 (maxDistance, 1));

		for (int i = 0; i <= resolution; i++) {
			float t = (float)i / (float)resolution;
			arcArray [i] = calculateArcPoint (t, maxDistance);
		}

		return arcArray;
	}

	Vector3 calculateArcPoint(float t , float maxDistance)
	{
		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle)));
		return new Vector3 (x, y);
	}
}
