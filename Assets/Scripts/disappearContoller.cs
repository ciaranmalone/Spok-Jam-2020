using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappearContoller : MonoBehaviour
{
    [SerializeField] private string phase;
    [SerializeField] private GameObject hiddenObjects;
    void Start()
    {
        GameEvents.current.onPhaseChange += handleDisappear;
    }
    void handleDisappear(string phase)
    {
        if(phase == this.phase) {
            hiddenObjects.SetActive(false);
            GameEvents.current.onPhaseChange -= handleDisappear;
        }
    }
}
