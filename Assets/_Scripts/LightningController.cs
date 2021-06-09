using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [SerializeField] private Color fogColor;
    [SerializeField] private Material skyBox;
    [SerializeField] private Material lightningSkyBox;
    [SerializeField] private float FogIntensity = .7f;

    void Start()
    {
        RenderSettings.fogColor = fogColor;
        RenderSettings.skybox = skyBox;
        RenderSettings.fogDensity = FogIntensity;
        RenderSettings.fog = true;
        StartCoroutine("lightning");
    }

    private IEnumerator lightning()
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                RenderSettings.fog = false;
                RenderSettings.skybox = lightningSkyBox;
                yield return new WaitForSeconds(Random.Range(.05f, .15f));

                RenderSettings.fog = true;
                RenderSettings.skybox = skyBox;
                yield return new WaitForSeconds(Random.Range(.05f, .15f));
            }

            for (float i = 0; i < FogIntensity; i+=.01f)
            {
                float fogColor = 1-(i + (1 - FogIntensity));
                
                RenderSettings.fogDensity = i;
                RenderSettings.fogColor =new Color(0, 0, 0, fogColor);
                yield return new WaitForSeconds(.05f);
            }
      
            RenderSettings.fog = true;
            RenderSettings.skybox = skyBox;
            yield return new WaitForSeconds(Random.Range(3f, 9f));
        }
    }
}