using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPickUp : MonoBehaviour
{
    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
