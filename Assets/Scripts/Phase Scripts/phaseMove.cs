using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseMove : MonoBehaviour
{
    [SerializeField] private int phase;
    [SerializeField] private new Vector3 newLocation;

    void Start()
    {
        GameEvents.current.onPhaseChange += moveObject;
    }

    void moveObject(int phase)
    {
        if(phase == this.phase) 
        {
            transform.Translate(newLocation, Space.World);
            GameEvents.current.onPhaseChange -= moveObject;

        }
    }
}
