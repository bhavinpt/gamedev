using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_color_on_trigger : MonoBehaviour {

    public Color OnColor,OffColor,CurrentColor;
    Renderer rend;
    Light ButtonLight;
    void Start () {

         rend = GetComponentInParent<Renderer>();
         ButtonLight = GetComponentInParent<Light>();

        rend.material.color = OffColor;
        ButtonLight.color = OffColor;

        CurrentColor = OffColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
            rend.material.color = OffColor;
            ButtonLight.color = OffColor;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            rend.material.color = OnColor;
            ButtonLight.color = OnColor;
        }
    }
}
