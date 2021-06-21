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
        if (GameManager.gameManager)
        {
            if (GameManager.gameManager.phase >= phase) transform.parent = null;
            else return;
        }
        
        
        //since someone (unity) is a dubmass gamemabager is null when you're starting the game for the very first time, literally nothing i can do about it, sorry
        if(phase==PhaseID.Phase1) transform.parent = null; //yes i know i start at phase 1 
    }
}
