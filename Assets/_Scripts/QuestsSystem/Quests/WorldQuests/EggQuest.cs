using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldQuests;

public class EggQuest : MonoBehaviour
{

    [SerializeField]
    ProgrammaticQuests.QuestObjectName itemToCollideWith;

    private void OnTriggerEnter(Collider other)
    {
        QuestItem qi = other.GetComponent<QuestItem>();
        if(qi && qi.Quest_Object_Name==itemToCollideWith)
        {
            Destroy(gameObject);
        }
    }

}
