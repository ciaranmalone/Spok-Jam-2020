using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ControlPPWithDistance : MonoBehaviour
{
    PostProcessVolume postProcessVolume;
    private Vector3 target;
    private Transform PlayerTransform;
    float Distance;
    [SerializeField] float multiplier = 5;

    void Start()
    {
        PlayerTransform = PlayerMovement.Instance.transform;
        postProcessVolume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        Distance = 1 / (target.magnitude)* multiplier;
        target = transform.position - PlayerTransform.position;
        postProcessVolume.weight = Distance;
    }
}
