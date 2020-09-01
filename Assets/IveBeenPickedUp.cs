using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IveBeenPickedUp : MonoBehaviour
{
    private AudioSource audioData;
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    void OnTransformParentChanged()
    {
        if(SelectItem.pickedUp == true) 
        {
            audioData.Play();
            print("hello");

        } else{
            audioData.Stop();

            foreach (Transform child in transform)
            {
                child.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
