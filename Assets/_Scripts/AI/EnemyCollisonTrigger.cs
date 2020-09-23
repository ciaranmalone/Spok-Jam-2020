using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private string[] phases;
    private int indexChoice;
    private float fogAmount = 0f;
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            indexChoice = Random.Range (0, phases.Length);
            GameEvents.current.PhaseChange(phases[indexChoice]);
            print("number" + indexChoice);
        }
    }
    void Update() {
        RenderSettings.fogDensity = fogAmount;
    }
}