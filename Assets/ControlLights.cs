using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLights : MonoBehaviour
{

    private Projector projector;
    private GameObject[] lights;
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Lights");
        projector = GetComponent<Projector>();
        DisableLights();
    }

    public void EnableLights()
    {
        projector.enabled = false;
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }

    public void DisableLights()
    {
        projector.enabled = true;
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

}
