using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

    public bool FirstButton;
    Two_button_door Door;

    private void Start()
    {
        Door = transform.parent.GetChild(0).GetComponent<Two_button_door>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (FirstButton)
        {
            Door.FirstButtonMoved(true);
        }

        else
        {
            Door.SecondButtonMoved(true);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (FirstButton)
        {
            Door.FirstButtonMoved(false);
        }

        else
        {
            Door.SecondButtonMoved(false);
        }

    }
}
