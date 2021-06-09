using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDoorsOpen : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;
    [SerializeField] AudioClip[] sounds;
    private AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

   private void OnTriggerEnter(Collider other) 
   {
        LeanTween.moveLocal(doorLeft, new Vector3(-3, 0, 0), 2f).setEaseOutCubic();
        LeanTween.moveLocal(doorRight, new Vector3(6, 0, 0), 2f).setEaseOutCubic();
        src.PlayOneShot(sounds[0]);
   }

   private void OnTriggerExit(Collider other)
   {
     LeanTween.moveLocal(doorLeft, new Vector3(0, 0, 0), 2f).setEaseOutCubic();
     LeanTween.moveLocal(doorRight, new Vector3(3, 0, 0), 2f).setEaseOutCubic();
        src.PlayOneShot(sounds[1]);

    }
}
