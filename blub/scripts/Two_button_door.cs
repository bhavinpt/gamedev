using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_button_door : MonoBehaviour {

    /// <summary>
    /// Assuming that the door is closed initially
    /// </summary>

    public float SwitchSpeed;
    public bool OpensUp;
    bool FirstButtonState , SecondButtonState;
    Rigidbody Door;
    Vector3 DoorOpenPos , DoorClosePos , MovePos;

    void Start () {
        Door = GetComponent<Rigidbody>();
        float DoorSize = GetComponent<Renderer>().bounds.size.y;

        DoorClosePos = Door.position;
        DoorOpenPos = Door.position;

        if (OpensUp)
        {
            DoorOpenPos.y += DoorSize;
        }
        else
        {
            DoorOpenPos.y -= DoorSize;
        }
	}
	
	void FixedUpdate () {
        
        if (FirstButtonState || SecondButtonState)
        {
            MovePos=Vector3.MoveTowards(Door.position , DoorOpenPos ,Time.fixedDeltaTime * SwitchSpeed );
        }
        else
        {
            MovePos = Vector3.MoveTowards(Door.position, DoorClosePos, Time.fixedDeltaTime * SwitchSpeed);
        }

        Door.MovePosition(MovePos);
    }

    public void FirstButtonMoved (bool state)
    {
        FirstButtonState = state;
    }

    public void SecondButtonMoved(bool state)
    {
        SecondButtonState = state;
    }
}
