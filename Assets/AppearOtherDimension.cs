using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOtherDimension : MonoBehaviour
{
    [SerializeField] GameObject OtherDimension;
    
    private void Start() {
        OtherDimension.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        OtherDimension.SetActive(true);

    }
}
