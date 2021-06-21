using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGameTrigger : MonoBehaviour
{
    [SerializeField]
    ProgrammaticQuests.QuestObjectName obj = ProgrammaticQuests.QuestObjectName.PLAYER;

    private void OnTriggerEnter(Collider other)
    {
        WorldQuests.QuestItem qst = other.GetComponent<WorldQuests.QuestItem>();
        if(qst && qst.Quest_Object_Name==obj)
        {
            GameManager.gameManager.StartPhaseLoop();
        }
    }
}
