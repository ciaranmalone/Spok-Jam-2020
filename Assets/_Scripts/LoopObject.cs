using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopObject : MonoBehaviour
{

    [Range(1, 10)]
    [SerializeField]
    int loop;

    private void Start()
    {
        if (GameManager.gameManager.loopCount < loop) Destroy(gameObject);
    }

}
