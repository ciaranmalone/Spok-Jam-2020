using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimation : MonoBehaviour
{
    public enum state { idle, walking, looking, alert, chase};
    [SerializeField]
    state currentState;
    [SerializeField]
    Animator anim;



    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case state.idle:
                anim.SetBool("Idle", true);
                anim.SetBool("Walking", false);
                anim.SetBool("Looking", false);
                anim.SetBool("Alert", false);
                anim.SetBool("Chase", false);
                break;

            case state.walking:
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", true);
                anim.SetBool("Looking", false);
                anim.SetBool("Alert", false);
                anim.SetBool("Chase", false);
                break;

            case state.looking:
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Looking", true);
                anim.SetBool("Alert", false);
                anim.SetBool("Chase", false);
                break;

            case state.alert:
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Looking", false);
                anim.SetBool("Alert", true);
                anim.SetBool("Chase", false);
                break;

            case state.chase:
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Looking", false);
                anim.SetBool("Alert", false);
                anim.SetBool("Chase", true);
                break;
        }
    }

    public void setState(state newState)
    {
        currentState = newState;
    }
}
