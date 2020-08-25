using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompleteObjective : MonoBehaviour
{
    [SerializeField] private int objective = 0;
    
    private void OnTriggerEnter(Collider other) {
        GameEvents.current.ObjectiveComplete(objective);
        Destroy(gameObject);
    }
}
