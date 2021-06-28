using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectOnTrigger : MonoBehaviour
{
    [SerializeField] GameObject GameobjectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameobjectToDisable !=null)
        {
            GameobjectToDisable.SetActive(false);
        }
    }
}