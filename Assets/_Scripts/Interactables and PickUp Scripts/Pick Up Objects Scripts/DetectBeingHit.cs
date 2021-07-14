using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBeingHit : MonoBehaviour
{
    private bool watched = false;
    private float timer = 0.0f;
    [SerializeField] private GameObject spookyFace;
    SelectItem si;

    void Start()
    {
        if (spookyFace)
        {
            spookyFace.SetActive(false);
            si = PlayerMovement.Instance.GetComponentInChildren<SelectItem>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        watched = si.getLookedAtObject() == gameObject;
        
        timer += watched? Time.deltaTime : -timer;
        
        spookyFace.SetActive(timer >= 5);
    }
}
