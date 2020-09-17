using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    [SerializeField] int taskSheet = 0;
    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;
    [SerializeField] private bool isPhone = false;

    private bool played = false;
    private Component[] phoneCallScripts;
    private Animator anim;
    private AudioSource audioData;

    private void Start() {
        if(GetComponent<Animator>() != null){
            anim = GetComponent<Animator>();
        }

        if(GetComponent<AudioSource>() != null){
            audioData = GetComponent<AudioSource>();
        }
    }

    public void handleInteraction(){

        if(GetComponent<AudioSource>() != null & !isPhone) {
            audioData.Play();
        }

        if(GetComponent<Animator>() != null) {
            if (played){
                anim.Play(animationOne);
            }
            else{
                anim.Play(animationTwo);
            }
            played = !played;
        }

        if (taskSheet != 0) {
            GameEvents.current.nextTaskSheet(taskSheet);
            Destroy(gameObject);     
        }

        if(isPhone){

            //getting the currect phone script for the current phase
            phoneCallScripts = GetComponents(typeof (phoneCallScript));
            
            foreach (phoneCallScript script in phoneCallScripts){
                if(script.phase == ("phoneCall" + GameEvents.current.getMissionHandler().getCurrentPhase()) & script.Ringing == true) {
                    script.phoneAnswered = true;
                }
            }
        }
    }
}
