using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOnLeave : MonoBehaviour
{
    [SerializeField] private string animation = "DimensionDoorClose";
    [SerializeField] private Animator anim;
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("DisappearDimension");
        }
    }

    IEnumerator DisappearDimension()
    {
        anim.Play(animation);
        yield return new WaitForSeconds(1);
        this.transform.parent.gameObject.SetActive(false);

    }
}
