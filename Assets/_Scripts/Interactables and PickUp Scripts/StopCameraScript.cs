using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraScript : MonoBehaviour
{
    private CameraSpin cameraSpin;
    private void Start() => cameraSpin = GetComponent<CameraSpin>();
    public void turnOffScript() => cameraSpin.enabled = false;
}
