using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private float lowPitch  = 0.5f;
    [SerializeField] private float highPitch = 1.5f;
    private AudioSource AudioSource;
    private float hitVol;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

     private void OnCollisionEnter(Collision collision)
    {
        hitVol = collision.relativeVelocity.magnitude * 0.2f;
        AudioSource.pitch = Random.Range(lowPitch, highPitch);
        AudioSource.PlayOneShot(crashSound, hitVol);

        if(collision.relativeVelocity.magnitude < 5) {
            GameEvents.current.SoundMade(transform.position);
        }
    }
}
