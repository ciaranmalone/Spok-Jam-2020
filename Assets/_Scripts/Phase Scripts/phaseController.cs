using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseController : MonoBehaviour
{
    [SerializeField]
    private string phase;
    void Start()
    {
        GameEvents.current.onPhaseChange += PhaseChangeHandler;
    }
    private void PhaseChangeHandler(string phase){
        if(phase == this.phase){
            LeanTween.moveLocalY(gameObject, 10f, 1f).setEaseOutQuad();
        }
    }

    private void OnDestroy() {
        GameEvents.current.onPhaseChange -= PhaseChangeHandler;
    }
}
