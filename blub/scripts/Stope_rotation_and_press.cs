using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stope_rotation_and_press : MonoBehaviour {

    public GameObject Press, SecondRotor;
    Vector3 PressInitPos;

	void Start () {
        if (Press==null)
        {
            Debug.Log("forgot to put press in reference");
        }
        PressInitPos = Press.transform.position;
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("rotor"))
        {
            Destroy(GetComponent<ConstantForce>());
            Destroy(GetComponent<Simple_collide_kill>());

            if (SecondRotor!=null)
            {
                Destroy(SecondRotor.GetComponent<ConstantForce>());
                //Destroy(SecondRotor.GetComponent<Simple_collide_kill>());
            }
            Destroy(Press.GetComponent<Hammer>());
            Rigidbody PressRb = Press.GetComponent<Rigidbody>();
            PressRb.velocity = Vector3.zero;
            PressRb.transform.position = PressInitPos;
        }
    }
}
