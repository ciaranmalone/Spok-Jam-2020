using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorAnimate : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioData;
    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;
    [SerializeField] private ProgrammaticQuests.QuestID questToBreakOn;
    [SerializeField] ProgrammaticQuests.PhaseID phase;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.gameManager.isQuestComplete(questToBreakOn) || GameManager.gameManager.phase > phase)
        {
            anim.Play(animationOne);
            audioData.Play();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        anim.Play(animationTwo);
    }
}
