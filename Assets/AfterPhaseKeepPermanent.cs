using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPhaseKeepPermanent : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent = null;
    }
}
