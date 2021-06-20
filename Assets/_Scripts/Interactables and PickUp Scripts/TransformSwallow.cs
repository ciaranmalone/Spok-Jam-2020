using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSwallow : MonoBehaviour
{
    [SerializeField] private ProgrammaticQuests.QuestObjectName objectiveItem = ProgrammaticQuests.QuestObjectName.MALK;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WorldQuests.QuestItem>() != null && other.GetComponent<WorldQuests.QuestItem>().Quest_Object_Name == objectiveItem)
        {
            other.transform.parent = this.transform.parent;
        }
    }
}
