using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeAllJointsOnTrigger : MonoBehaviour {


    public GameObject[] ObjectsWithHingeJoints;
    public float DelayBeforeRemoving;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head") || other.CompareTag("body"))
        {
            StartCoroutine(DelayedRemoving());
        }
    }

    void RemoveAllJoints()
    {
        foreach (GameObject JointToDestroy in ObjectsWithHingeJoints)
        {
            HingeJoint[] All_Joints = JointToDestroy.GetComponents<HingeJoint>();
            foreach (var aJoint in All_Joints)
            {
                Destroy(aJoint);
            }
        }
    }

    IEnumerator DelayedRemoving()
    {
        yield return new WaitForSeconds(DelayBeforeRemoving);
        RemoveAllJoints();

    }
}
