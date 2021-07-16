using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    AudioSource src;
    string areaName;

    void OnTriggerEnter(Collider area)
    {
        if (area.gameObject.name == "inside")
        {
            src.clip = clips[0];
            src.Play();
        }
        if (area.gameObject.name == "outside")
        {
            src.clip = clips[1];
            src.Play();
        }
    }

    public void TurnOffSound()
    {
        src.Stop();
    }

    public void TurnOnSound()
    {
        src.Play();
    }
}
