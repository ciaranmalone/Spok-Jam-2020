﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimation : MonoBehaviour
{
    public enum state { idle, run, stand, roar, chase};
    [SerializeField]
    state currentState;
    [SerializeField]
    Animator anim;



    // Update is called once per frame
    void Update()
    {
        resetBools();
        switch (currentState)
        {
            case state.idle:
                anim.SetBool("idle", true);
                break;

            case state.run:
                anim.SetBool("run", true);
                break;

            case state.stand:
                anim.SetBool("stand", true);
                break;

            case state.roar:
                anim.SetBool("roar", true);
                break;

            case state.chase:
                anim.SetBool("chase", true);
                break;
        }
    }

    void resetBools()
    {
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        anim.SetBool("stand", false);
        anim.SetBool("roar", false);
        anim.SetBool("chase", false);
    }
    public void setState(state newState)
    {
        currentState = newState;
    }
}
