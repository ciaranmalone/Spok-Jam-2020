using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    bool on = true;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(flickerTimer());
    }

    IEnumerator flickerTimer()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, 3));

        GetComponent<Light>().enabled = false;


        yield return new WaitForSecondsRealtime(Random.Range(0.05f, 1));

        GetComponent<Light>().enabled = true;
        StartCoroutine(flickerTimer());
    }
}
