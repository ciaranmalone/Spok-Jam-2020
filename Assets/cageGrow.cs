using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cageGrow : MonoBehaviour
{
    [SerializeField] private string animationOne;
    [SerializeField] private string animationTwo;

    private Animator anim;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.Play(animationOne);
        StartCoroutine(shrink());
    }

    IEnumerator shrink()
    {
        yield return new WaitForSeconds(10f);
        anim.Play(animationTwo);
    }
}
