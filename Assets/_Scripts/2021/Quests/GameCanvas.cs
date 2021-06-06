using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProgrammaticQuests;
using System;

public class GameCanvas : MonoBehaviour
{

    [SerializeField]
    GameObject questPrefab, questPivot;
    [SerializeField]
    int questTextOffset;
    GameObject[] uiQuests;


    private void Start()
    {
    }



    /// <summary>
    /// Create new list of mission objectives in the UI.
    /// 
    /// It is asking for the dictionary of current quests, because it will strikethrough ones already completed
    /// </summary>
    internal void MakeObjectives(Dictionary<QuestID, bool>.KeyCollection quests)
    {
        uiQuests = new GameObject[quests.Count];
        int questOffset = 0;
        foreach (QuestID quest in quests)
        {
            //creating the UI object
            Vector3 pivot = questPivot.transform.localPosition;
            GameObject UIRepresentation = Instantiate(questPrefab, pivot, questPivot.transform.rotation, transform);
            uiQuests[questOffset] = UIRepresentation;

            ////getting the objective and it's controller
            //Objective tempObjective = quest.GetComponent<Objective>();
            //ObjectiveController controller = UIRepresentation.GetComponent<ObjectiveController>();

            ////getting proper visuals in the ui
            //step 1: get data
            Quest curr = GameManager.gameManager.qs.getQuest(quest);

            //step 2: assign data
            UIRepresentation.GetComponent<RectTransform>().localPosition = new Vector3(pivot.x, pivot.y - (questTextOffset * (questOffset)));
            UIRepresentation.GetComponent<TextMeshProUGUI>().text = curr.count > 1 ? $"{curr.description} (0/{curr.count})" : curr.description;

            ////setting the mission counter to both TriggerCompleteObjective and ObjectiveController scripts
            //quest.GetComponent<TriggerCompleteObjective>().setObjectiveOffset(missionOffset);
            //controller.setObjectiveOffset(missionOffset);
            //controller.setAudioclip(tempObjective.AudioClip);

            questOffset++;
        }
    }

    internal void QuestCompleteC(int where)
    {
        uiQuests[where].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
    }

    /// <summary>
    /// Update the counter in the quest, Canvas Side
    /// </summary>
    internal void QuestUpdateC(Quest quest, int where, int count, string desc = null)
    {
        if (quest.count == 1)
        {
            if (desc == null) return;
            uiQuests[where].GetComponent<TextMeshProUGUI>().text = desc;
        }
        else
        {
            uiQuests[where].GetComponent<TextMeshProUGUI>().text = $"{(desc == null ? quest.description : desc)} ({count}/{quest.count})";
        }
    }
}
