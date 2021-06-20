using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldQuests;

namespace WorldQuests
{
    public class EggQuest : MonoBehaviour
    {

        [SerializeField]
        ProgrammaticQuests.QuestObjectName itemToCollideWith;
        [SerializeField]
        bool canvas;
        [SerializeField]
        internal ProgrammaticQuests.QuestID quest_id;

        private void OnTriggerEnter(Collider other)
        {
            if (!canvas)
            {
                QuestItem qi = other.GetComponent<QuestItem>();
                if (qi && qi.Quest_Object_Name == itemToCollideWith)
                {
                    FakeToFromAnimation ftfa = GetComponent<FakeToFromAnimation>();
                    if (ftfa) ftfa.Animate();
                    Destroy(gameObject);
                }
            }
        }
    }
}
