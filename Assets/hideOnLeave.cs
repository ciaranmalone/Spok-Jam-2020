using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOnLeave : MonoBehaviour
{
    [SerializeField] private string animation = "DimensionDoorClose";
    [SerializeField] private Animator anim;
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            this.transform.parent.gameObject.SetActive(false);
            anim.Play(animation);

        }
    }
}
