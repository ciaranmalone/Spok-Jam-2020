using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOtherDimension : MonoBehaviour
{
    [Header("Door Opening")]
    [SerializeField] GameObject OtherDimension;
    [SerializeField] private string animation = "DimensionDoorOpen";
    [SerializeField] private Animator anim;

    [Header("Phase Condition")]
    [SerializeField] private string phase;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform PlayerTeleport;

    private void Start() {
        OtherDimension.SetActive(false);
        GameEvents.current.onPhaseChange += handleAppearDimension;
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            OtherDimension.SetActive(true);
            anim.Play(animation);
        }
    }
    void handleAppearDimension(string phase)
    {
        if(phase == this.phase) {
            OtherDimension.SetActive(true);
            Player.transform.position = PlayerTeleport.position;

            anim.Play(animation);
        }
    }
}
