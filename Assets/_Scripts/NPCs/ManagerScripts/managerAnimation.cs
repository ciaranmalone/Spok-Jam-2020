using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerAnimation : MonoBehaviour
{
    public enum state { idle, talking};
    public state currentState;
    [SerializeField] Animator anim;

    void Update()
    {
        resetBools();
        switch (currentState)
        {
            case state.idle:
                anim.SetBool("Idle", true);
                break;

            case state.talking:
                anim.SetBool("Talking", true);
                break;
        }
    }

    void resetBools()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Talking", false);
    }
    public void setState(state newState)
    {
        currentState = newState;
    }
}
