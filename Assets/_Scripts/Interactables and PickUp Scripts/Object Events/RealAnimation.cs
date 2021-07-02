using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealAnimation : MonoBehaviour
{

    /// <summary>
    /// Required Object must have an animation component
    /// </summary>
    [SerializeField] GameObject objWithAnimation;
    Animator animator;
    /// <summary>
    /// I hate you
    /// </summary>
    [SerializeField]
    string animationName;

    /// <summary>
    /// Optional clip
    /// </summary>
    [SerializeField] AudioClip clip;
    bool playSound = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = objWithAnimation.GetComponent<Animator>();
        if (!animator) Debug.LogError("There is no animator in an object, please check this before proceeding");
    }

    internal void Animate()
    {
        //start animating here
        animator.Play(animationName);
        if (clip && playSound)
        {
            AudioSource auso = objWithAnimation.AddComponent<AudioSource>();
            auso.spatialBlend = 1;
            auso.minDistance = 10;
            auso.maxDistance = 60;
            auso.dopplerLevel = 0;
            auso.PlayOneShot(clip);
        }
    }

    public void disableSound()
    {
        playSound = false;
    }
}
