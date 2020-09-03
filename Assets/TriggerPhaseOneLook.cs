using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhaseOneLook : MonoBehaviour
{
    [SerializeField] private int phase;
   
    public void triggerPhase() {
        GameEvents.current.PhaseChange(phase);
    }
}
