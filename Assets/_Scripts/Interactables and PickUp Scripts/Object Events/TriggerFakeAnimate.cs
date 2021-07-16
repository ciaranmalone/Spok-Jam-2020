using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FakeToFromAnimation))]
public class TriggerFakeAnimate : MonoBehaviour
{
    [SerializeField]
    ProgrammaticQuests.QuestObjectName ObjectName = ProgrammaticQuests.QuestObjectName.PLAYER;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WorldQuests.QuestItem>().Quest_Object_Name == ObjectName)
        {
            GetComponent<FakeToFromAnimation>().Animate();

        }
    }
}
