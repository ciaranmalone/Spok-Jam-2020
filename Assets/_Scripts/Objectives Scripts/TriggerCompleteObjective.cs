using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompleteObjective : MonoBehaviour
{
    private int objective;
    [SerializeField] private string objectiveItem;
    [SerializeField] int itemsTotal = 1;
    int itemsCompleted = 0;
    private void OnTriggerEnter(Collider other) {

        /* 
         * TODO on trigger enter update text to x/y 
         */
        ObjectiveItem otherItem = other.GetComponent<ObjectiveItem>();
        
        if(otherItem != null) {
            if (otherItem.ItemName == objectiveItem)
            {
                itemsCompleted++;
                if (itemsTotal > 1)
                {
                    GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText + " (" + itemsCompleted + "/" + itemsTotal + ")"));
                }
                else
                {
                    GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText));
                }

                if (itemsCompleted >= itemsTotal)
                {
                    GameEvents.current.ObjectiveComplete(objective);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectiveItem>().ItemName == objectiveItem)
        {
            itemsCompleted--;
            if (itemsTotal > 1)
            {
                GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText + " (" + itemsCompleted + "/" + itemsTotal + ")"));
            }
            else
            {
                GameEvents.current.getMissionHandler().getCurrentPhaseScript().renameMission(objective, (GetComponent<Objective>().ObjectiveText));
            }
        }
    }

    public void setObjectiveOffset(int offset)
    {
        objective = offset;
    }

    public int getMissionsTotal()
    {
        return itemsTotal;
    }
}
