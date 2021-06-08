using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldQuests
{
    /// <summary>
    /// Quest trigger when an object enters the area
    /// 
    /// NOTE: might refactor it to just contain the programmatic quest itself rather references to few pieces of data
    /// </summary>
    public class Quest : MonoBehaviour
    {
        [SerializeField] 
        internal ProgrammaticQuests.QuestID quest_id;
        int timesCompleted = 0;
        ProgrammaticQuests.QuestObjectName object_name;
        int totalToComplete;

        private void Start()
        {
            ProgrammaticQuests.Quest quest = GameManager.gameManager.qs.getQuest(quest_id);
            totalToComplete = quest.count;
            object_name = quest.objectName;

        }


        private void OnTriggerEnter(Collider other)
        {
            QuestItem otherItem = other.GetComponent<QuestItem>();

            if (otherItem != null)
            {
                if (otherItem.Quest_Object_Name == object_name)
                {
                    if (otherItem.deTaggable) otherItem.tag = "Untagged";
                    timesCompleted++;
                    if (totalToComplete > 1)
                    {
                        GameManager.gameManager.QuestUpdateGM(quest_id, timesCompleted);
                    }

                    if (timesCompleted >= totalToComplete)
                    {
                        QuestCompleteWQ();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            QuestItem otherItem = other.GetComponent<QuestItem>();
            if (other.GetComponent<QuestItem>() != null && other.GetComponent<QuestItem>().Quest_Object_Name == object_name)
            {
                if (otherItem.deTaggable) other.tag = "pickUp";
                timesCompleted--;
                if (totalToComplete > 1)
                {
                    GameManager.gameManager.QuestUpdateGM(quest_id, timesCompleted);
                }
            }
        }

        /// <summary>
        /// Function to call to complete a quest, separate from the trigger 
        /// because if a quest ever needs to be done programmatically then 
        /// this function is nice :)
        /// </summary>
        public void QuestCompleteWQ()
        {
            GameManager.gameManager.QuestCompleteGM(quest_id);
            CallPostQuestEvent();
        }

        public void CallPostQuestEvent()
        {
            FakeToFromAnimation ftfa = GetComponent<FakeToFromAnimation>();
            if (ftfa)
            {
                if (GameManager.gameManager.loading)
                {
                    ftfa.disableSound();
                }
            }
            Destroy(gameObject);
        }

    }
}
