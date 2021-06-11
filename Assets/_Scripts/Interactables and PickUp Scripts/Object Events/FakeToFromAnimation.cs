using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeToFromAnimation : MonoBehaviour
{
    //[Header("Requirements")]
    [SerializeField] bool fromRequired;
    [SerializeField] bool playSound;

    //[Header("Objects")]
    [SerializeField] GameObject fromObject;
    [SerializeField] GameObject toObject;

    //[Header("Audio")]
    [SerializeField] AudioClip clip;


    private void Start()
    {
        if (fromRequired)
        {
            fromObject.SetActive(true);
        }
        toObject.SetActive(false);
    }
    private void OnDestroy()
    {
        if(fromRequired) fromObject.SetActive(false);
        toObject.SetActive(true);
        toObject.transform.parent = null;
        if (playSound)
        {
            AudioSource auso = toObject.AddComponent<AudioSource>();
            auso.spatialBlend = 1;
            auso.minDistance = 10;
            auso.maxDistance = 60;
            auso.dopplerLevel = 0;
            auso.PlayOneShot(clip);
        }
    }

    public void disableSound()
    {
        playSound = false;
    }
}
