using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    [SerializeField] private float angleA = 90f;
        [SerializeField] private float angleB = 0f;

    private void Start() {
        StartCoroutine(rotateCamera());
    }

    IEnumerator rotateCamera() {
        yield return new WaitForSeconds(5);
        LeanTween.rotateY(gameObject, angleA, 2f);

        yield return new WaitForSeconds(5);
        LeanTween.rotateY(gameObject, angleB, 2f);

        StartCoroutine(rotateCamera());

    }
}
