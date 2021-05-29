﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    [SerializeField] MissionHandler missionHandler;
    private void Awake() {
        //singleton for persistence - Evan
        if(!current)
            current = this;
        nextTaskSheet(0);
    }
    public event Action<string> onPhaseChange;
    public void PhaseChange(string phase){

        if (onPhaseChange != null){
            onPhaseChange(phase);
        }
    }

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

    public event Action<Vector3> onSoundMade;
    public void SoundMade(Vector3 location){

        if (onSoundMade != null){
            onSoundMade(location);
        }
    }

    /// <summary>
    /// Spawn the next task sheet in the world
    /// </summary>
    public void spawnNextNote()
    {
        missionHandler.getCurrentPhaseScript().spawnNextNote();
    }

    /// <summary>
    /// Creates new set of tasks in the UI
    /// </summary>
    /// <param name="sheet">The phase the game is entering into</param>
    public void nextTaskSheet(int sheet)
    {
        missionHandler.clearPhase(sheet - 1);
        missionHandler.startPhase(sheet);
    }

    /// <summary>
    /// Get everything mission related
    /// </summary>
    public MissionHandler getMissionHandler()
    {
        return missionHandler;
    }
}
