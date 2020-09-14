using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] phases;

    public void startPhase(int phase)
    {
        phases[phase].GetComponent<MissionScript>().MakeObjectives();
    }

    public void clearPhase(int phase)
    {
        if (phase < 0) return;
        phases[phase].GetComponent<MissionScript>().ClearObjectives();
    }
}
