using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProgrammaticQuests;

public class AfterPhaseKeepPermanent : MonoBehaviour
{

    [SerializeField]
    PhaseID phase = PhaseID.Phase2;

    private void OnEnable()
    {
        if(GameManager.gameManager  && GameManager.gameManager.phase >= phase) transform.parent = null;
    }
}
