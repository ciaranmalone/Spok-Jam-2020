using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogOn : MonoBehaviour
{ 

    private float fogAmount = 0f;
    [SerializeField] private Material skybox;
    [SerializeField] private Material blankSkybox;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fogDensity = fogAmount;
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.name == "FogOn")
        {
            RenderSettings.skybox = blankSkybox;
            StartCoroutine( ChangeFog( 0, .1f, 2f ) );
            RenderSettings.fog = true;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "FogOn")
        {
            RenderSettings.skybox = skybox;
            StartCoroutine( ChangeFog( .1f, 0f, 2f ) );
           // RenderSettings.fog = false;
        }
    }

    IEnumerator ChangeFog(float start, float end, float duration){
        float elapsed = 0.0f;

        while (elapsed < duration ) {
            fogAmount  = Mathf.Lerp( start, end, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        fogAmount = end;
    }
}
