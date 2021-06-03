using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper for events that happen when a quest is being interacted with
/// </summary>
[System.Serializable]
public class Quest
{
    [SerializeField]
    int id;
    [SerializeField]
    string name;
    [SerializeField]
    bool completed;
}
