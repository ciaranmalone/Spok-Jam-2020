using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private bool played = false;
    [SerializeField] int taskSheet = 0;
    [SerializeField] private string animationOne = "BigDoorOpen";
    [SerializeField] private string animationTwo = "BigDoorClosed";

    private Animator anim;
    private AudioSource audioData;

    private void Start() {
        anim = GetComponent<Animator>();

        if(GetComponent<AudioSource>() != null){
            audioData = GetComponent<AudioSource>();
        }
    }
    
    public void handleInteraction(){
        print("interacting");

        if(GetComponent<AudioSource>() != null){
            audioData.Play();
        }
        if (taskSheet != 0)
        {
            GameEvents.current.nextTaskSheet(taskSheet);
            Destroy(gameObject);
        }
        if (played){
            anim.Play(animationOne);
        }
        else{
            anim.Play(animationTwo);
        }
        played = !played;
        
    }
}
