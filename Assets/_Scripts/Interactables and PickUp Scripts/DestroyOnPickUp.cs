using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPickUp : MonoBehaviour
{
    public void DestroyThisObject()
    {
        FakeToFromAnimation[] ftfa = GetComponents<FakeToFromAnimation>();
        foreach (var item in ftfa)
        {
            item.Animate();
        }
        Destroy(this.gameObject);
    }
}
