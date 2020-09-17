using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] phases;
    int missionsRemaining;
    int currentPhase;
    MissionScript cachedCurrentMissionScript;

    public void startPhase(int phase)
    {
        currentPhase = phase;
        cachedCurrentMissionScript = phases[phase].GetComponent<MissionScript>();
        missionsRemaining = cachedCurrentMissionScript.MakeObjectives();
    }

    public void clearPhase(int phase)
    {
        if (phase < 0) return;
        missionsRemaining = 0;
        phases[phase].GetComponent<MissionScript>().ClearObjectives();
        cachedCurrentMissionScript = null;
    }

    public void completeMission()
    {
        missionsRemaining--;
    }

    public int getRemainingMissionCount()
    {
        return missionsRemaining;
    }

    public int getCurrentPhase()
    {
        return currentPhase;
    }

    public MissionScript getCurrentPhaseScript()
    {
        return cachedCurrentMissionScript;
    }
}
