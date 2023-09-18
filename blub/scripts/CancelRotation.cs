using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelRotation : MonoBehaviour {

    public Transform TargetParent;
    Vector3 PosDiff;

    private void Start()
    {
        PosDiff = TargetParent.position - transform.position;
    }
    
    private void LateUpdate()
    {
        transform.position = TargetParent.position - PosDiff;
    }
}
