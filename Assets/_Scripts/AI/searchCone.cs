using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchCone : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask everyMask;
    [SerializeField] private float coneAngle = 45f;

    private Camera cam;
    private Transform camTransform;

    private Vector3 target;
    private float angle;
    private void Start()
    {
        cam = PlayerMovement.Instance.GetComponentInChildren<Camera>();
        camTransform = cam.transform;
    }
    void Update()
    {
        /**
         * hide enemy if angle between player and enemy is less then than coneAngle
         */
        target = transform.position - camTransform.position;
        angle = Vector3.Angle(target, camTransform.forward);
        cam.cullingMask = angle < 45f? enemyMask : everyMask;
    }
}
