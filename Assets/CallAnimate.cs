using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimate : MonoBehaviour
{
    public void CallFtFa()
    {
        FakeToFromAnimation[] ftfa = GetComponents<FakeToFromAnimation>();
        foreach (var item in ftfa)
        {
            item.Animate();
        }
    }
}
