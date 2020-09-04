using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogOn : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.name == "FogOn")
        {
            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.25f;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "FogOn")
        {
            RenderSettings.fogDensity = 0;
            RenderSettings.fog = false;
        }
    }
}
