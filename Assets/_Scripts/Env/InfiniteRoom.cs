using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoom : MonoBehaviour
{
    [SerializeField]
    GameObject to;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.transform.parent = gameObject.transform;
            Vector3 lp = other.gameObject.transform.localPosition;
            Quaternion lr = other.gameObject.transform.localRotation;
            other.gameObject.transform.parent = to.transform;
            other.gameObject.transform.localPosition = lp;
            other.gameObject.transform.localRotation = lr;
            other.gameObject.transform.parent = null;
        }
    }
}
