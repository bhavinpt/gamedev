using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineFromObjects : MonoBehaviour {

    LineRenderer Wire;
    public Transform[] Objects;

    void Start()
    {
        Wire = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Wire.SetPosition(i, Objects[i].position);
        }
    }
}
