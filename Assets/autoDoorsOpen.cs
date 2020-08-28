using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDoorsOpen : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;
   private void OnTriggerEnter(Collider other) 
   {
        LeanTween.moveX(doorLeft, -6, 1f);
        LeanTween.moveX(doorRight, 6, 1f);
   }

   private void OnTriggerExit(Collider other)
   {
        LeanTween.moveX(doorLeft, 0, 1f);
        LeanTween.moveX(doorRight, 3, 1f);
   }
}
