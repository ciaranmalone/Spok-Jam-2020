using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }
    public event Action<string> onPhaseChange;
    public void PhaseChange(string phase){

        if (onPhaseChange != null){
            onPhaseChange(phase);
        }
    }

    public event Action<int> onObjectiveComplete;
    public void ObjectiveComplete(int objective){

        if (onObjectiveComplete != null){
            onObjectiveComplete(objective);
        }
    }
}
