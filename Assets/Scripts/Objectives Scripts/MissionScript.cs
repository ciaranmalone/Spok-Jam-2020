using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionScript : MonoBehaviour
{
    //1.
    [SerializeField]
    GameObject[] objectives;
    //2.
    [Header("Canvas Components")]
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    GameObject objectivePrefab, objectivePivot;
    [SerializeField][Tooltip("The distance between each objective in the UI")]
    int objectiveTextOffset = 140;

    GameObject[] uiobjs;

    public int MakeObjectives()
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
            UIRepresentation.GetComponent<TextMeshProUGUI>().text = tempObjective.ObjectiveText;

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

}
