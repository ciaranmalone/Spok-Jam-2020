using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private string PickUpTag = "pickUp";
    [SerializeField] private string InteractableTag = "interactable";
    [SerializeField] private string SeeTriggerTag = "lookAtMe";
    [SerializeField] private GameObject lightPoint;

    public static bool pickedUp = false;
    private Transform selection;
    private Transform selected;

    void FixedUpdate()
    {
        RaycastHit hit;
        lightPoint.SetActive(false);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f, ~IgnoreMe))
        {
            selection = hit.transform;

            //if the item is a pick up item
            if(selection.CompareTag(PickUpTag) && !pickedUp && hit.distance < 5f) {
                lightPoint.SetActive(true);
                lightPoint.transform.position = hit.point;

                if(selection.gameObject.GetComponent<DetectBeingHit>() != null) {
                    selection.gameObject.GetComponent<DetectBeingHit>().imBeingLookedAt();
                }

                if(Input.GetButtonDown("Fire2")) {
                    selected = selection;
                    pickedUp = true;

                    //make object the child of the player
                    selected.parent = gameObject.transform;
                    selected.localPosition = new Vector3(0, -1, 2);
                    selected.localRotation = Quaternion.identity;

                    //stop its phyiscs so it will stop giggling around
                    if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                       selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            
            //if the item is a interactable item
            } else if (selection.CompareTag(InteractableTag) && !pickedUp && hit.distance < 5f) {
                lightPoint.SetActive(true);
                lightPoint.transform.position = hit.point;

                if(Input.GetButtonDown("Fire2")) {
                    selected = selection;
                    
                    if(selected.gameObject.GetComponent<interactable>() != null) {
                        selected.gameObject.GetComponent<interactable>().handleInteraction();
                    }
                }
            } else if (selection.CompareTag(SeeTriggerTag)) {
                selected = selection;

                if(selected.gameObject.GetComponent<TriggerPhaseOneLook>() != null) {
                    selected.gameObject.GetComponent<TriggerPhaseOneLook>().triggerPhase();
                }
            }
        }

        //drop object that is picked up                       
        if(Input.GetButtonDown("Fire1") && pickedUp){
            pickedUp = false;
            selected.parent = null;

            if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            }  else { selected.gameObject.AddComponent<Rigidbody>(); }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }
}IE
9+

