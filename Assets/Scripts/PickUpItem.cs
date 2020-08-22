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
                    selected.parent = gameObject.transform;
                    selected.localPosition = new Vector3(0, -1, 2);
                    selected.localRotation = Quaternion.identity;
                    
                    if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                       selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
        }
    
        if(Input.GetButtonDown("Fire1") && pickedUp){
            selected.parent = null;
            pickedUp = false;

            if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            }
            else {
                selected.gameObject.AddComponent<Rigidbody>();
            }
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }
}
