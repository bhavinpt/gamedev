using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour {
    private bool Striking,hit;
    ParticleSystem ElectricLight;
    Light Lightcolor;
    float TargetIntensity;
    public float ChangeSpeed;

    // Use this for initialization
    void Start () {
        ElectricLight = GetComponent<ParticleSystem>();
        Lightcolor = GetComponentInChildren<Light>();
        hit = true;
        ElectricLight.Stop();
        Lightcolor.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (hit)
        {
            hit = false;
            StartCoroutine(Strike());
        }
        Lightcolor.intensity = Mathf.MoveTowards(Lightcolor.intensity,TargetIntensity,Time.deltaTime*ChangeSpeed);

    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("bulb") & Striking)
        {
            Debug.Log("bulb killed by lightning");
        }
        
    }

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Striking = !Striking;
        if (Striking)
        {
            ElectricLight.Play();
            TargetIntensity = Random.Range(3.5f, 4.5f);
        }
        else
        {
            ElectricLight.Stop();
            TargetIntensity = 0;
        }

        hit = true;
    }
}
