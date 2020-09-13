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
    [SerializeField]
    float floorCheckLength;
    public int soundType;
    [SerializeField]
    string floorType;
    RaycastHit floorCheckHit;
    [SerializeField]
    LayerMask floorCheckMask;
    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, -transform.up, out floorCheckHit, floorCheckLength, floorCheckMask);
        floorType = floorCheckHit.collider.gameObject.tag;
        switch(floorType)
        {
            case ("grass"):
                soundType = 0;
                break;
            case ("tile"):
                soundType = 1;
                break;
            case ("path"):
                soundType = 2;
                break;
        }
        if(movementScript.isWalking == true)
        {
            switch (soundType)
            {
                case (0):
                    sources[0].volume = volume;
                    sources[1].volume = 0;
                    sources[2].volume = 0;
                    break;
                case (1):
                    sources[0].volume = 0;
                    sources[1].volume = volume;
                    sources[2].volume = 0;
                    break;
                case (2):
                    sources[0].volume = 0;
                    sources[1].volume = 0;
                    sources[2].volume = volume;
                    break;
            }
            sources[soundType].volume = volume;
        }
        else
        {
            sources[0].volume = 0;
            sources[1].volume = 0;
            sources[2].volume = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up * floorCheckLength);
    }
}
