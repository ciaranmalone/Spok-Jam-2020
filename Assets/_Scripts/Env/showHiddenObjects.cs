using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHiddenObjects : MonoBehaviour
{
    [SerializeField] private GameObject HiddenObjects;
    void Start()
    {
        HiddenObjects.SetActive(false);
    }

    public void showObjects()
    {
        HiddenObjects.SetActive(true);
    }
}
