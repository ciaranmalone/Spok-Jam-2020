using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    private Light light; 
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(flickerTimer());
    }

    IEnumerator flickerTimer()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, 3));

        light.enabled = false;

        yield return new WaitForSecondsRealtime(Random.Range(0.05f, 1));

        light.enabled = true;
        StartCoroutine(flickerTimer());
    }
}
