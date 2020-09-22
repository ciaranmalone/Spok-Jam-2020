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
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.red;
            StartCoroutine( ChangeFog( 0, 1f, 2f ) );
        }
    }
    void Update() {
        RenderSettings.fogDensity = fogAmount;
    }

    IEnumerator ChangeFog(float start, float end, float duration){
        float elapsed = 0.0f;

        while (elapsed < duration ) {
            fogAmount  = Mathf.Lerp( start, end, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        fogAmount = end;
        GameEvents.current.PhaseChange(phases[indexChoice]);
    }
}
