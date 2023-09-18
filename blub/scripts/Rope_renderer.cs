using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_renderer : MonoBehaviour {

    LineRenderer Line;
    public int length;
    public GameObject RopePrefab;
    int Parts;

    void Start () {

        for (int i = 0; i < length; i++)
        {

        }

        Parts = transform.childCount;
        Line = GetComponent<LineRenderer>();
        Line.positionCount = Parts;
	}
	
	void Update () {

        for (int i = 0; i < Parts; i++)
        {
            Line.SetPosition(i, transform.GetChild(i).position);
        }
	}
}
