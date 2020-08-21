using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private int phase;

   private void Start() {
        GameEvents.current.onPhaseChange += AnimationHandler;
    }

    private void AnimationHandler(int phase){
        if(phase == this.phase){
            LeanTween.scaleZ(gameObject, -1000f, 100f).setEaseOutQuad();

        }
    }
}
