using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBeingHit : MonoBehaviour
{
    private bool watched = false;
    private float timer = 0.0f;
    [SerializeField] private GameObject spookyFace;

    void Start()
    {
        spookyFace.SetActive(false);
    }
    public void imBeingLookedAt() {
        watched = true;
        timer += Time.deltaTime;
        print("timer " + timer);
    }

    void FixedUpdate()
    {
        if(watched == false) {
            timer = 0.0f;
        }

        watched = false;

        if(timer >= 5) {
            spookyFace.SetActive(true);
        }else {
            spookyFace.SetActive(false);
        }
    }
}
