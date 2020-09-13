using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSoundManager : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movementScript;
    [SerializeField]
    AudioSource[] sources;
    [SerializeField]
    float volume;
    public int floorType;
    // Update is called once per frame
    void Update()
    {
        if(movementScript.isWalking == true)
        {
            sources[floorType].volume = volume;
        }
        else
        {
            sources[floorType].volume = 0;
        }
    }
}
