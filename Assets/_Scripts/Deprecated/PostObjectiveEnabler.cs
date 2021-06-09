using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostObjectiveEnabler : MonoBehaviour
{
    [SerializeField] GameObject what;

    private void OnDestroy()
    {
        what.SetActive(true);
    }
}
