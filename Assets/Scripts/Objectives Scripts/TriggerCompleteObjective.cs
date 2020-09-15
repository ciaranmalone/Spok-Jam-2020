﻿using System.Collections;
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
        if (other.GetComponent<ObjectiveItem>().ItemName == objectiveItem)
        {
            itemsCompleted++;
            GameEvents.current.getMissionHandler().getCurrentPhaseObject().GetComponent<MissionScript>().renameMission(objective, (GetComponent<Objective>().ObjectiveText + " (" + itemsCompleted + "/" + itemsTotal + ")"));
            if (itemsCompleted >= itemsTotal)
            {
                GameEvents.current.ObjectiveComplete(objective);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectiveItem>().ItemName == objectiveItem)
        {
            itemsCompleted--;
            GameEvents.current.getMissionHandler().getCurrentPhaseObject().GetComponent<MissionScript>().renameMission(objective, (GetComponent<Objective>().ObjectiveText + " (" + itemsCompleted + "/" + itemsTotal + ")"));
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
