using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
      [SerializeField]
    private int phase = 1;
    
    private void OnTriggerEnter(Collider other) {
        GameEvents.current.PhaseChange(phase);
    }
}
