using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private int phase;   

    [SerializeField] private bool on = true;

    Material m_Material;
    private Light spotLight;


     void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        spotLight = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
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

        m_Material.DisableKeyword("_EMISSION");
        spotLight.enabled = false;

        if (!on){
            yield break; 
        }

        yield return new WaitForSecondsRealtime(Random.Range(0.05f, 1));
        if(on) { 
            m_Material.EnableKeyword("_EMISSION");
            spotLight.enabled = true;

            StartCoroutine(flickerTimer());
        }
    }
}
