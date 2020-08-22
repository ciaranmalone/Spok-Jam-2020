using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private  LayerMask IgnoreMe;
    [SerializeField] private string SelectableTag = "pickUp";

      private bool pickedUp = false;
    private Transform selection;
    private Transform selected;

    
   
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~IgnoreMe))
        {
            selection = hit.transform;
            if(selection.CompareTag(SelectableTag)) {
                if(Input.GetButtonDown("Fire2") && !pickedUp) {
                    selected = selection;
                    pickedUp = true;
                    selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    selected.parent = gameObject.transform;
                    selected.localPosition = new Vector3(0, -1, 2);
                    selected.rotation = Quaternion.identity;
                }
            }
        }
    
        if(Input.GetButtonDown("Fire1")){
            print("hello");
            selected.parent = null;
            pickedUp = false;
            selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

    }
}
