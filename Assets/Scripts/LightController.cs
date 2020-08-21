using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private int phase;   

    [SerializeField]
    private bool on = true;

    private Light pointLight;
    private Light spotLight;


     void Start()
    {
        pointLight = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        spotLight = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Light>();
        GameEvents.current.onPhaseChange += toggleLights;
        StartCoroutine(flickerTimer());
    }

    private void toggleLights(int phase) 
    {
        if(phase == this.phase) {
            on = false;
        }
    }

    IEnumerator flickerTimer()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, 3));

        pointLight.enabled = false;
        spotLight.enabled = false;

        if (!on){
            yield break; 
        }

        yield return new WaitForSecondsRealtime(Random.Range(0.05f, 1));
        if(on) { 
            pointLight.enabled = true;
            spotLight.enabled = true;

            StartCoroutine(flickerTimer());
        }
    }
}
