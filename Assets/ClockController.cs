using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    private ProgrammaticQuests.PhaseID startPhase;
    private float rotation;
    void Start()
    {
        startPhase = GameManager.gameManager.phase;
    
        transform.localRotation = new Quaternion(0,rotation,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if( startPhase != GameManager.gameManager.phase)
        {
            Debug.Log($"rot {startPhase} {GameManager.gameManager.phase}");
            rotation += 30;
            Debug.Log($" rot amount {rotation}");
            transform.Rotate(new Vector3(0,1,0) *30);
            startPhase = GameManager.gameManager.phase;
        }
    }
}
