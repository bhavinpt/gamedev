using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {
    ParticleSystem Flames ;
    public float TimeToRelease, TimeToHit ,FlameIntensity ;
    Light FlameLight;
    private bool hit,Died;
    float CurrentTime , CurrentIntensity;

    void Start()
    {
        Flames = GetComponent<ParticleSystem>();
        FlameLight = transform.parent.GetComponentInChildren<Light>();
        hit = true;
        if (TimeToRelease == 0)
        {
            TimeToRelease = TimeToHit / 3;
        }
        if (TimeToHit == 0)
        {
            TimeToHit = TimeToRelease;
        }
        CurrentTime = TimeToRelease;
    }

    void FixedUpdate()
    {   
        if (hit)
        {
            if (CurrentTime == TimeToHit)
            {
                Flames.Play(true);
                FlameLight.enabled = true;
                CurrentIntensity = FlameIntensity;
            }
            else
            {
                Flames.Stop(true);
                CurrentIntensity = 0; ;
            }
            hit = false;
            StartCoroutine(FlameWait());
            CurrentTime = (TimeToHit + TimeToRelease) - CurrentTime;        // toggling , starting with toggling time to hit
        }

        FlameLight.intensity = Mathf.MoveTowards(FlameLight.intensity, CurrentIntensity, TimeToHit*4* Time.fixedDeltaTime);
    }

    IEnumerator FlameWait()
    {
        yield return new WaitForSeconds(CurrentTime);
        hit = true;

    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("bulb"))
        {
            if (FlameLight.intensity > FlameIntensity / 10 && Died == false)
            {
                Debug.Log("Blub killed by flamethrower");
                Died = true;
            }
        }
    }
}
