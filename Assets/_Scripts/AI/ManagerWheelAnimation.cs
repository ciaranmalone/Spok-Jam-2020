using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWheelAnimation : MonoBehaviour
{
    public enum state { idle, wheelSpin};
    public state currentState;
    [SerializeField] Animator anim;

    void Update()
    {
        resetBools();
        switch (currentState)
        {
            case state.idle:
                anim.SetBool("idle", true);
                break;

            case state.wheelSpin:
                anim.SetBool("wheelSpin", true);
                break;
        }
    }

    void resetBools()
    {
        anim.SetBool("idle", false);
        anim.SetBool("wheelSpin", false);
    }
    public void setState(state newState)
    {
        currentState = newState;
    }
}
