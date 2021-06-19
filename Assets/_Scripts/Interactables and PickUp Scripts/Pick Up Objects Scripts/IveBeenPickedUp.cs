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

            while (transform.childCount != 0)
            {
                transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                transform.GetChild(0).parent = transform.parent;
            }
            GameManager.gameManager.CreateBonusQuest(GetComponent<WorldQuests.EggQuest>().quest_id);
            Destroy(gameObject);
        }
    }
}
