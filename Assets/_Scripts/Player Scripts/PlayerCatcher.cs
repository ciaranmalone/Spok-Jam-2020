using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    [SerializeField] Vector3 teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.position = teleportPoint;
            other.gameObject.GetComponent<CharacterController>().Move(Vector3.zero);
        }
    }
}
