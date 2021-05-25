using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeToFromAnimation : MonoBehaviour
{
    [SerializeField] GameObject fromObject, toObject;

    private void Start()
    {
        fromObject.SetActive(true);
        toObject.SetActive(false);
    }
    private void OnDestroy()
    {
        fromObject.SetActive(false);
        toObject.SetActive(true);
    }
}
