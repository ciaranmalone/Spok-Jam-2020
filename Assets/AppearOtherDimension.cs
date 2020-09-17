using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOtherDimension : MonoBehaviour
{
    [SerializeField] GameObject OtherDimension;
    [SerializeField] private string animation = "DimensionDoorOpen";
    [SerializeField] private Animator anim;
    private void Start() {
        OtherDimension.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            OtherDimension.SetActive(true);
            anim.Play(animation);
        }
    }
}
