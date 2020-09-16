using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    string objectiveText;
    [SerializeField]
    AudioClip audioClip;

    public string ObjectiveText { get => objectiveText; }
    public AudioClip AudioClip { get => audioClip; }
}
