using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake() {
        //singleton for persistence - Evan
        if(!current)
            current = this;
    }
    public event Action<string> onPhaseChange;
    public void PhaseChange(string phase){

        if (onPhaseChange != null){
            onPhaseChange(phase);
        }
    }
    /**
    public event Action<int> onObjectiveComplete;
    public void ObjectiveComplete(int objective){
        if (onObjectiveComplete != null)
        {
            print("epic big chungus");
            onObjectiveComplete(objective);
            missionHandler.completeMission();
            print(missionHandler.getRemainingMissionCount());
            if(missionHandler.getRemainingMissionCount() <= 0)
            {
                PhaseChange("phoneCall"+missionHandler.getCurrentPhase());
            }
        }
    }
    */

    public event Action<Vector3> onSoundMade;
    public void SoundMade(Vector3 location){

        if (onSoundMade != null){
            onSoundMade(location);
        }
    }
}
