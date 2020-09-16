using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearController : MonoBehaviour
{
    [SerializeField] private string phase;

    [SerializeField] private GameObject hiddenObjects;
    void Start()
    {
        hiddenObjects.SetActive(false);
        GameEvents.current.onPhaseChange += handleAppear;
    }
    void handleAppear(string phase)
    {
        if(phase == this.phase) {
            hiddenObjects.SetActive(true);
            GameEvents.current.onPhaseChange -= handleAppear;
        }
    }
}
