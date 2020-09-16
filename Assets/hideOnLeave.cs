using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOnLeave : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        print("disappear");
        this.transform.parent.gameObject.SetActive(false);
    }
}
