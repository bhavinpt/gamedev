using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWire : MonoBehaviour {

    LineRenderer Wire;

	void Start () {
        Wire = GetComponent<LineRenderer>();		
	}

	void LateUpdate () {

        Wire.positionCount = transform.childCount;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Wire.SetPosition(i, transform.GetChild(i).position);    
        }
	}
}
