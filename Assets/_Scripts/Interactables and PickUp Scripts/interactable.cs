using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    [SerializeField] bool taskSheet = false;
    //Objects to be destroyed from previous phase by persistence script
    [SerializeField] private string[] previousPhasePurges;
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

    public void handleInteraction(bool destroy = false){
        Debug.Log("hello "+ played);

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

        if (taskSheet) {
            GameManager.gameManager.CreatePhase(true);
            //GameEvents.current.nextTaskSheet(taskSheet);
            //Destroy(gameObject);
            gameObject.SetActive(false);

            if (destroy)
            {
                foreach (string objName in previousPhasePurges)
                {
                    Destroy(GameObject.Find(objName));
                }
            }
        }

        if(isPhone){

            //getting the currect phone script for the current phase
            phoneCallScripts = GetComponents(typeof (phoneCallScript));
            
            foreach (phoneCallScript script in phoneCallScripts){
                if(script.phase == ("phoneCall" + (int)GameManager.gameManager.phase) & script.Ringing == true) {
                    script.phoneAnswered = true;
                }
            }
        }
    }
}
