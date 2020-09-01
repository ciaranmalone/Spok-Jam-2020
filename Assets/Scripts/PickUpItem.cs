﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private string PickUpTag = "pickUp";
    [SerializeField] private string InteractableTag = "interactable";
    [SerializeField] private GameObject lightPoint;

    private bool pickedUp = false;
    private Transform selection;
    private Transform selected;

    void Update()
    {
        RaycastHit hit;
        lightPoint.SetActive(false);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f, ~IgnoreMe))
        {
            selection = hit.transform;

            //if the item is a pick up item
            if(selection.CompareTag(PickUpTag) && !pickedUp) {
                lightPoint.SetActive(true);
                lightPoint.transform.position = hit.point;

                if(Input.GetButtonDown("Fire2")) {
                    selected = selection;
                    pickedUp = true;

                    selected.parent = gameObject.transform;
                    selected.localPosition = new Vector3(0, -1, 2);
                    selected.localRotation = Quaternion.identity;

                    if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                       selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            
            //if the item is a interactable item
            } else if (selection.CompareTag(InteractableTag)) {

                lightPoint.SetActive(true);
                lightPoint.transform.position = hit.point;

                if(Input.GetButtonDown("Fire2")) {

                    selected = selection;
                    
                    if(selected.gameObject.GetComponent<interactable>() != null) {
                        selected.gameObject.GetComponent<interactable>().handleInteraction();
                    }
                }
            }
        }

        //drop object that is picked up                       selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;

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
