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

    /// <summary>
    /// Create new list of mission objectives in the UI.
    /// </summary>
    /// <param name="canvas">To get the missions be the parent of the canvas</param>
    /// <param name="objectivePrefab">The placeholder UI representation of an objective, then renamed and functional in the script</param>
    /// <param name="objectivePivot">The anchor from where the missions get added in the UI</param>
    /// <param name="objectiveTextOffset">How far apart are the missions</param>
    /// <returns>How many missions are there</returns>
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
            UIRepresentation.GetComponent<TextMeshProUGUI>().text = totalItems > 1 ? $"{tempObjective.ObjectiveText} (0/{totalItems})" : tempObjective.ObjectiveText;

            //setting the mission counter to both TriggerCompleteObjective and ObjectiveController scripts
            objective.GetComponent<TriggerCompleteObjective>().setObjectiveOffset(missionOffset);
            controller.setObjectiveOffset(missionOffset);
            controller.setAudioclip(tempObjective.AudioClip);
            
            missionOffset++;
        }
        return objectives.Length;
    }

    /// <summary>
    /// Flush UI objectives, use before creating new objectives
    /// </summary>
    public void ClearObjectives()
    {
        foreach (GameObject objective in uiobjs)
        {
            Destroy(objective);
        }
    }

    /// <summary>
    /// Set active the next task sheet
    /// </summary>
    public void spawnNextNote()
    {
        nextNote.SetActive(true);
    }

    /// <summary>
    /// Renames mission in the ui, for example to update it's progress
    /// </summary>
    public void renameMission(int index, string text)
    {
        uiobjs[index].GetComponent<TextMeshProUGUI>().text = text;
    }

}
