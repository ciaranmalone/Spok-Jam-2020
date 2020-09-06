using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseMove : MonoBehaviour
{
    [SerializeField] private string phase;
    [SerializeField] private new Vector3 newLocation;

    void Start()
    {
        GameEvents.current.onPhaseChange += moveObject;
    }

    void moveObject(string phase)
    {
        if(phase == this.phase) 
        {
            transform.Translate(newLocation, Space.World);
            GameEvents.current.onPhaseChange -= moveObject;

        }
    }
}
