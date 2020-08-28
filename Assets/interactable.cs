using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private bool played = false;
    [SerializeField] private string animationOne = "BigDoorOpen";
    [SerializeField] private string animationTwo = "BigDoorClosed";

    private Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
        anim.Play(animationOne);
    }
    public void playAnimation(){
        if(played){
            anim.Play(animationOne);
        }
        else{
            anim.Play(animationTwo);
        }
        played = !played;
    }
}
