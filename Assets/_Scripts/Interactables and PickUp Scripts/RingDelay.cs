using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingDelay : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float TimeDelay = 3f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        StartCoroutine("DelayRing");
    }
    IEnumerator DelayRing()
    {
        yield return new WaitForSeconds(TimeDelay);
        Debug.Log("start music");
        audioSource.Play();
    }
}