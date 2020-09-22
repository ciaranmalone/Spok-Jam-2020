using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGrowPhase : MonoBehaviour
{

    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;
    [SerializeField] private string phase;

    private Animator anim;

    void Start()
    {
        GameEvents.current.onPhaseChange += handleStoreGrowth;

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    void handleStoreGrowth(string phase)
    {
        if(phase == this.phase)
        {
            anim.Play(animationOne);
        }
    }
}
