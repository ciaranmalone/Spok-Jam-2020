using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class backDoorAnimate : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioData;
    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;
    [SerializeField] private ProgrammaticQuests.PhaseID[] phasesToBreakOn;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(phasesToBreakOn.Any(t => t == GameManager.gameManager.phase))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                child.gameObject.SetActive(true);
            }
        }
        else{
            anim.Play(animationOne);
            audioData.Play();

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                child.gameObject.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!phasesToBreakOn.Any(t => t == GameManager.gameManager.phase))
        {
            anim.Play(animationTwo);

        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            child.gameObject.SetActive(false);
        }
    }
}
