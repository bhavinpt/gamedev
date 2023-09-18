using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnFreezeInsideTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            Rigidbody Rb = other.GetComponent<Rigidbody>();
            Rb.constraints = RigidbodyConstraints.None;

            Debug.Log("unfreezed rb constraints for " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            Debug.Log("freezed rb constraints for " + other.name);
            Rigidbody Rb = other.GetComponent<Rigidbody>();
            Rb.constraints = RigidbodyConstraints.FreezeRotation |  RigidbodyConstraints.FreezePositionZ;
        }
    }
}
