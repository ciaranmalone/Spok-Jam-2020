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

        } else{
            audioData.Stop();
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                transform.GetChild(0).parent = null;
                            
            }
          
            
        }
    }
}
