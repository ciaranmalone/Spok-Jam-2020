using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backDoorAnimate : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioData;
    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;
    [SerializeField] ProgrammaticQuests.PhaseID phase;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.gameManager.phase != phase)
        {
            anim.Play(animationOne);
            audioData.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.gameManager.phase != phase)
        {
            anim.Play(animationTwo);
        }
    }
}
