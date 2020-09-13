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

    void Start()
    {
        
    }

    void Update()
    {
        switch (areaName)
        {
            case ("inside"):
                src.clip = clips[0];
                if(src.isPlaying == false) { src.Play(); }
                break;
            case ("outside"):
                src.clip = clips[1];
                if (src.isPlaying == false) { src.Play(); }
                break;
        }    
    }

    void OnTriggerEnter(Collider area)
    {
        if (area.gameObject.name == "inside") { areaName = "inside"; }
        if (area.gameObject.name == "outside") { areaName = "outside"; }
    }
}
