using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAudioController : MonoBehaviour
{
    private AudioSource audioSourcePlayOnce, audioSourceLoop;
    [SerializeField] private AudioClip patrol, spotted, chasing, caught;
    void Start()
    {
        Debug.Log("<color=cyan>(___)___):::::::D~~~</color>");
        audioSourcePlayOnce = gameObject.AddComponent<AudioSource>();
        audioSourceLoop = gameObject.AddComponent<AudioSource>();

        audioSourceLoop.loop = true;
        audioSourceLoop.spatialBlend = 1;
        audioSourceLoop.minDistance = 0;
        audioSourceLoop.maxDistance = 10;
        audioSourceLoop.playOnAwake = true;
        audioSourceLoop.rolloffMode = AudioRolloffMode.Custom;
    	
        audioSourcePlayOnce.loop = false;
        audioSourcePlayOnce.spatialBlend = 0;
        audioSourcePlayOnce.playOnAwake = false;
    }

    internal void Patrol() 
    {
        if(audioSourceLoop.clip == patrol) return;
        audioSourceLoop.maxDistance = 10;
        audioSourceLoop.clip = patrol;
        audioSourceLoop.Play();
    }

    internal void Spotted() 
    {
        audioSourcePlayOnce.PlayOneShot(spotted);
    }

    internal void Chasing() 
    {
        if(audioSourceLoop.clip == chasing) return;
        audioSourceLoop.maxDistance = 20;
        audioSourceLoop.clip = chasing;
        audioSourceLoop.Play();
        audioSourcePlayOnce.PlayOneShot(spotted);

    }

    internal void Caught() 
    {
        audioSourcePlayOnce.PlayOneShot(caught);

    }
}
