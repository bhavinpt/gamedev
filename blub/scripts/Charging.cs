using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charging : MonoBehaviour {
    Transform Wire, Switch, Socket;
    Button ChargButton;
    private bool Buttonclicked,charging ;
    Light BulbLight;
    public float ChargingSpeed,MaxCharging, MoveSpeedToSocket;
    Rigidbody BulbRb;
    GameObject ButtonObject;

    void Start () {
        Socket = transform.GetChild(0);
        Switch = transform.GetChild(1);
        Wire = transform.GetChild(2);

        ButtonObject = GameObject.FindGameObjectWithTag("charger_switch").gameObject;
        ButtonObject.SetActive(false);
        ChargButton = ButtonObject.GetComponent<Button>();

        BulbLight = GameObject.FindGameObjectWithTag("bulb_light").GetComponent<Light>();
        BulbRb = GameObject.FindGameObjectWithTag("bulb").GetComponent<Rigidbody>();
    }
	

	void Update () {
        if (charging)
        {
            Wire.localScale = Vector3.MoveTowards(Wire.localScale, Vector3.one, Time.deltaTime * MoveSpeedToSocket);
            Switch.localRotation = Quaternion.RotateTowards(Switch.localRotation, Quaternion.Euler(-180, 0, 0), Time.deltaTime * 140f);
            BulbRb.position = Vector2.MoveTowards(BulbRb.position, Socket.position, MoveSpeedToSocket * Time.deltaTime);
            Debug.Log(BulbRb.position-Socket.position);
            if ((Vector2)BulbRb.position==(Vector2)Socket.position)
            {
                BulbLight.intensity += Time.deltaTime * ChargingSpeed;
                BulbLight.intensity = Mathf.Clamp(BulbLight.intensity, 0f, MaxCharging);
                if (BulbLight.intensity >= MaxCharging)
                {
                    charging = false;
                    BulbRb.useGravity = true;
                }
            }
        }
        else
        {
            Switch.localRotation = Quaternion.RotateTowards(Switch.localRotation, Quaternion.identity, Time.deltaTime * 140f);
            Wire.localScale = Vector3.MoveTowards(Wire.localScale, new Vector3(1,1,0.1f), Time.deltaTime * MoveSpeedToSocket);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bulb"))
        {
            ButtonObject.SetActive ( true );                             // show button
            ChargButton.onClick.AddListener(() => ButtonReady());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bulb"))
        {
            ButtonObject.SetActive(false);
            Buttonclicked = false;
            charging = false;
            ChargButton.onClick.RemoveAllListeners();
        }
    }

    void ButtonReady()      // button clicked
    {
        Buttonclicked = true;
        charging =!charging;        // toggle charging
        BulbRb.useGravity = !charging;
        Debug.Log("button clicked "+charging);


    }
}
