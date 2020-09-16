using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 5;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask everyMask;
    private float currentHitObject;
    Camera cam;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1, transform.TransformDirection(Vector3.forward), out hit, 100, 1 << LayerMask.NameToLayer("Enemy")) || Physics.SphereCast(transform.position, sphereRadius, transform.TransformDirection(Vector3.forward), out hit, 100, 1 << LayerMask.NameToLayer("Enemy"))) {
           
            currentHitObject = hit.distance;
            cam.cullingMask = enemyMask;
        } 
        else {
            cam.cullingMask = everyMask;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * currentHitObject);
        Gizmos.DrawWireSphere(transform.position + transform.forward * currentHitObject, sphereRadius);
    
    }
}
