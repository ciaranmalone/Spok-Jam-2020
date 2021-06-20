using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpOnLoadAfterQuest : MonoBehaviour
{
    [SerializeField]
    ProgrammaticQuests.QuestID quest;
    [SerializeField]
    ProgrammaticQuests.PhaseID phase;

    void Start()
    {
        if (!GameManager.gameManager) return;
        if (GameManager.gameManager.isQuestComplete(quest))
        {
            if (phase == ProgrammaticQuests.PhaseID.Phase1) //only one where there is no mobile phone to do stuff in
            { 
                GameManager.gameManager.spawnNextTaskSheet();
            }

            Destroy(gameObject);
        }
        if (GameManager.gameManager.phase > phase) Destroy(gameObject);
    }


}
