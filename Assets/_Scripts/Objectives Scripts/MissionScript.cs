using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionScript : MonoBehaviour
{
    //1.
    [SerializeField]
    GameObject nextNote;
    [SerializeField]
    GameObject[] objectives;
    
    GameObject[] uiobjs;

    public int MakeObjectives(Canvas canvas, GameObject objectivePrefab, GameObject objectivePivot, int objectiveTextOffset)
    {
        uiobjs = new GameObject[objectives.Length];
        int missionOffset = 0;
        foreach (GameObject objective in objectives)
        {
            //creating the UI object
            Vector3 pivot = objectivePivot.transform.localPosition;
            GameObject UIRepresentation = GameObject.Instantiate(objectivePrefab, pivot, objectivePivot.transform.rotation, canvas.transform);
            uiobjs[missionOffset] = UIRepresentation;
            //getting the objective and it's controller
            Objective tempObjective = objective.GetComponent<Objective>();
            ObjectiveController controller = UIRepresentation.GetComponent<ObjectiveController>();

            //getting proper visuals in the ui
            UIRepresentation.GetComponent<RectTransform>().localPosition = new Vector3(pivot.x, pivot.y - (objectiveTextOffset * (missionOffset)));
            int totalItems = objective.GetComponent<TriggerCompleteObjective>().getMissionsTotal();
            UIRepresentation.GetComponent<TextMeshProUGUI>().text = totalItems > 1 ? (tempObjective.ObjectiveText + " (0/"+totalItems + ")") : tempObjective.ObjectiveText;

            //setting the mission counter to both TriggerCompleteObjective and ObjectiveController scripts
            objective.GetComponent<TriggerCompleteObjective>().setObjectiveOffset(missionOffset);
            controller.setObjectiveOffset(missionOffset);
            controller.setAudioclip(tempObjective.AudioClip);
            
            missionOffset++;
        }
        return objectives.Length;
    }

    public void ClearObjectives()
    {
        foreach (GameObject objective in uiobjs)
        {
            Destroy(objective);
        }
    }

    public void spawnNextNote()
    {
        nextNote.SetActive(true);
    }

    public void renameMission(int index, string text)
    {
        uiobjs[index].GetComponent<TextMeshProUGUI>().text = text;
    }

}
