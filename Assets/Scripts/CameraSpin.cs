using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    private void Start() {
        StartCoroutine(rotateCamera());
    }

    IEnumerator rotateCamera() {
        yield return new WaitForSeconds(5);
        LeanTween.rotateY(gameObject, 90f, 2f);

        yield return new WaitForSeconds(5);
        LeanTween.rotateY(gameObject, 0f, 2f);

        StartCoroutine(rotateCamera());

    }
}
