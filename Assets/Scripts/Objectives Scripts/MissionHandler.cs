using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] phases;
    int missionsRemaining;
    int currentPhase;

    public void startPhase(int phase)
    {
        currentPhase = phase;
        missionsRemaining = phases[phase].GetComponent<MissionScript>().MakeObjectives();
    }

    public void clearPhase(int phase)
    {
        if (phase < 0) return;
        missionsRemaining = 0;
        phases[phase].GetComponent<MissionScript>().ClearObjectives();
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

    public GameObject getCurrentPhaseObject()
    {
        return phases[currentPhase];
    }
}
