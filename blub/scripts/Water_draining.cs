using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_draining : MonoBehaviour {

    public Transform Water;
    public float DrainingSpeed;
    public bool DestroyOnEmpty;

    private bool StartDraining , Called;
    private float TargetScale;

    void Start () {

        if (Water==null)
        {
            Debug.Log("you forgot to set target for water button");
        }
	}
	
	
	void Update () {
        if (StartDraining)
        {
            Vector3 _WaterScale=Water.localScale;
            _WaterScale.y = Mathf.MoveTowards(Water.localScale.y, TargetScale, DrainingSpeed*Time.deltaTime);
            Water.localScale = _WaterScale;

            if (Water.localScale.y == 0f)  
            {   
                StartDraining = false;
                if (DestroyOnEmpty)
                {
                    Destroy(Water.gameObject);
                    this.enabled = false;
                }
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if ( ( collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("body" ) ) && Called == false)
        {
            Called = true;
            StartCoroutine(CollideDelay());
            //Debug.Log("called   at  " + Time.time.ToString("F10") + " with "+ collision.gameObject.name);

            if (TargetScale==0f)
            {
                TargetScale = 1f;
                //Debug.Log("water TargetScale is 1 ");
            }
            else
            {
                TargetScale = 0f;
                //Debug.Log("water TargetScale is 0 ");
            }

            //TargetScale = Mathf.Abs(1 - TargetScale);   // toggling between 0 and 1
            //Debug.Log("water TargetScale = " + TargetScale);

            StartDraining = true;
        }

    }
    IEnumerator CollideDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Called = false;
    }
}
