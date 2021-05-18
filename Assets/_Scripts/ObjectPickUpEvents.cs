using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPickUpEvents : MonoBehaviour
{
    public UnityEvent ObjectPickUpEvent;

    public void scaleObject()
    {
        transform.localScale =  new Vector3(2, 2, 2);
    }

    public void skinnyObject()
    {
        transform.localScale = new Vector3(1, 5, 1);
    }
}
