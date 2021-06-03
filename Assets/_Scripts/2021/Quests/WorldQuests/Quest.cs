using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldQuests
{
    /// <summary>
    /// Quest trigger when an object enters the area
    /// 
    /// NOTE: THIS IS NOT DONE YET
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
                    timesCompleted++;
                    if (totalToComplete > 1)
                    {
                        //GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, $"{GetComponent<Objective>().ObjectiveText} ({timesCompleted}/{itemsTotal})");
                    }
                    else
                    {
                        //GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText));
                    }

                    if (timesCompleted >= totalToComplete)
                    {
                        completeQuest();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<QuestItem>() != null && other.GetComponent<QuestItem>().Quest_Object_Name == object_name)
            {
                timesCompleted--;
                if (totalToComplete > 1)
                {
                    //GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, $"{GetComponent<Objective>().ObjectiveText} ({timesCompleted}/{itemsTotal})");
                }
                else
                {
                    //GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText));
                }
            }
        }

        public void completeQuest()
        {
            GameManager.gameManager.QuestComplete(quest_id);
            //TODO run post-event activity here
            Destroy(gameObject);
        }

    }
}
