using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPhaseStarter : MonoBehaviour
{
    [SerializeField]
    ProgrammaticQuests.QuestObjectName obj = ProgrammaticQuests.QuestObjectName.PLAYER;

    private void OnTriggerEnter(Collider other)
    {
        print($"i'm a big box trigger collider, who entered me? {other.name}");
        WorldQuests.QuestItem qi = other.GetComponent<WorldQuests.QuestItem>();
        if (qi && qi.Quest_Object_Name == obj) GameManager.gameManager.CreatePhase(true);
    }
}
