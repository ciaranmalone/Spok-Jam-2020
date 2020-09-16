using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCounter : MonoBehaviour
{
    private List<int> CompleteObjectives = new List<int>();
    void Start()
    {
        GameEvents.current.onObjectiveComplete += ObjectiveAddCount;
    }

    void ObjectiveAddCount(int objective) 
    {
        CompleteObjectives.Add(objective);
    }
}
