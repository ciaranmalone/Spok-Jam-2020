using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseTrigger : MonoBehaviour
{
    [SerializeField] private string phase;
    
    private void OnTriggerEnter(Collider other) {
        GameEvents.current.PhaseChange(phase);
        Destroy(gameObject);
    }
}
