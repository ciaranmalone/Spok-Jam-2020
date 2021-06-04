using System;
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

    [Header("Canvas Components")] [SerializeField]
    private string canvasObjectName;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    GameObject objectivePrefab, objectivePivot;
    [SerializeField]
    [Tooltip("The distance between each objective in the UI")]
    int objectiveTextOffset = 140;

    private void Awake()
    {
        if(!canvas)
            canvas = GameObject.Find(canvasObjectName + "_Saved").GetComponent<Canvas>();
    }

    public void startPhase(int phase)
    {
        currentPhase = phase;
        phases[phase].gameObject.SetActive(true);
        cachedCurrentMissionScript = phases[phase].GetComponent<MissionScript>();
        missionsRemaining = cachedCurrentMissionScript.MakeObjectives(canvas, objectivePrefab, objectivePivot, objectiveTextOffset);
    }

    public void clearPhase(int phase)
    {
        print("Clearing phase: "+phase+" "+phases[phase].name);
        if (phase < 0) return;
        missionsRemaining = 0;
        phases[phase].GetComponent<MissionScript>().ClearObjectives();
        phases[phase].gameObject.SetActive(false);
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
    /// <summary>
    /// Get current mission phase
    /// </summary>
    /// <returns></returns>
    public int getCurrentPhase()
    {
        return currentPhase;
    }
    /// <summary>
    /// Get the script that handles the missions
    /// </summary>
    public MissionScript getCurrentPhaseScript()
    {
        return cachedCurrentMissionScript;
    }
}
