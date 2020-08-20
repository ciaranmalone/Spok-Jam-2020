using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch : MonoBehaviour
{
    bool on = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            on = !on;

            if (on == true)
            {
                StartCoroutine(flickerTimer());
            }
        }
    }

    IEnumerator flickerTimer()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, 3));

        if (on) { GetComponent<Light>().enabled = false; }
        else { yield break; }

        yield return new WaitForSecondsRealtime(Random.Range(0.05f, 1));
        if(on) { 
            GetComponent<Light>().enabled = true;
            StartCoroutine(flickerTimer());
        }

    }
}
