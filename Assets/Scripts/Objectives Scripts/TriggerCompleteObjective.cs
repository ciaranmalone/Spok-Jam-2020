using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompleteObjective : MonoBehaviour
{
    private int objective;
    [SerializeField] private GameObject objectiveItem;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == objectiveItem)
        {
            GameEvents.current.ObjectiveComplete(objective);
            Destroy(gameObject);
        }
    }

    public void setObjectiveOffset(int offset)
    {
        objective = offset;
    }
}
