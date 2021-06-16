using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProgrammaticQuests;

public class AfterPhaseKeepPermanent : MonoBehaviour
{

    [SerializeField]
    PhaseID phase = PhaseID.Phase2;
    bool start = false;

    private void Start()
    {
        start = true;
    }
    private void OnEnable()
    {
        if(start && GameManager.gameManager.phase == phase) transform.parent = null;
    }
}
