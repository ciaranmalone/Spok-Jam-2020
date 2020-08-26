using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompleteObjective : MonoBehaviour
{
    [SerializeField] private int objective = 0;
    [SerializeField] private GameObject objectiveItem;
    private void OnTriggerEnter(Collider other) {
        print(other.gameObject.name);
        if (other.gameObject == objectiveItem)
        {
            GameEvents.current.ObjectiveComplete(objective);
            Destroy(gameObject);
        }
    }
}
