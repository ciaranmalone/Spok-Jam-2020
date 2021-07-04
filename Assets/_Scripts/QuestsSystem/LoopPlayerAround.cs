using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPlayerAround : MonoBehaviour
{
    internal enum dir { NORTH, SOUTH, EAST, WEST };
    [SerializeField] private dir portalDir;
    [SerializeField] private Transform otherPortal;
    [SerializeField] bool LoopOnXAxis;
    [SerializeField] bool LoopOnZAxis;
    private LongFieldsPhoneHandler longFieldsPhoneHandler;

    void Start() => longFieldsPhoneHandler = FindObjectOfType<LongFieldsPhoneHandler>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (LoopOnXAxis)
            {
                other.transform.position = new Vector3(otherPortal.position.x, other.transform.position.y, other.transform.position.z);
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 2);
            }
            else if (LoopOnZAxis)
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, otherPortal.position.z);
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 2);
            }

            longFieldsPhoneHandler.MovePhone(portalDir);
        }
    }
}
