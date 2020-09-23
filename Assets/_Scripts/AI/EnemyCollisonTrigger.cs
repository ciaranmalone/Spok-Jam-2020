using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private string[] phases;
    private int indexChoice;
    private float fogAmount = 0f;

    private bool cooldown = false;
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" & !cooldown)
        {
            indexChoice = Random.Range (0, phases.Length);
            GameEvents.current.PhaseChange(phases[indexChoice]);
            StartCoroutine(handleCoolDown());
        }
    }
    void Update() {
        RenderSettings.fogDensity = fogAmount;
    }
    IEnumerator handleCoolDown()
    {
        cooldown = true;
        yield return new WaitForSeconds(10f);
        cooldown = false;
    }
}