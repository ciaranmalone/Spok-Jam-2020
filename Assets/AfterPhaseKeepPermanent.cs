using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPhaseKeepPermanent : MonoBehaviour
{

    bool first = true;

    private void OnEnable()
    {
        if(!first) transform.parent = null;
        first = false;
    }
}
