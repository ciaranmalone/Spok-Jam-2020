using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPlayerAround : MonoBehaviour
{
    [SerializeField] private Transform otherPortal;

    [SerializeField] bool LoopOnXAxis;
    [SerializeField] bool LoopOnZAxis;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && LoopOnXAxis)
        {
            other.transform.position = new Vector3(otherPortal.position.x, other.transform.position.y, other.transform.position.z);
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 2);
        } 
        else if(other.gameObject.CompareTag("Player") && LoopOnZAxis)
        {
            other.transform.position = new Vector3(other.transform.position.x, 0, otherPortal.position.z);
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 2);
        }
    }
}
