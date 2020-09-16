using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private string PickUpTag = "pickUp";
    [SerializeField] private string InteractableTag = "interactable";
    [SerializeField] private string SeeTriggerTag = "lookAtMe";
    
    [Header("Objects to display")]
    [SerializeField] private GameObject lightPointPickUp;
    [SerializeField] private GameObject PickUpIndicator;
    [SerializeField] private GameObject lightPointInteract;
    [SerializeField] private GameObject InteractIndicator;

    public static bool pickedUp = false;
    private Transform selection;
    private Transform selected;

    void Update()
    {
        RaycastHit hit;
        lightPointPickUp.SetActive(false);
        PickUpIndicator.SetActive(false);
        lightPointInteract.SetActive(false);
        InteractIndicator.SetActive(false);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f, ~IgnoreMe))
        {
            selection = hit.transform;

            //if the item is a pick up item
            if(selection.CompareTag(PickUpTag) && !pickedUp && hit.distance < 5f) {
                lightPointPickUp.SetActive(true);
                PickUpIndicator.SetActive(true);
                lightPointPickUp.transform.position = hit.point;

                if(selection.gameObject.GetComponent<DetectBeingHit>() != null) {
                    selection.gameObject.GetComponent<DetectBeingHit>().imBeingLookedAt();
                }

                if(Input.GetButtonDown("Select")) {
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
                lightPointInteract.SetActive(true);
                InteractIndicator.SetActive(true);

                lightPointInteract.transform.position = hit.point;

                if(Input.GetButtonDown("Select")) {
                    selected = selection;
                    
                    if(selected.gameObject.GetComponent<interactable>() != null) {
                        selected.gameObject.GetComponent<interactable>().handleInteraction();
                    }
                }
            } else if (selection.CompareTag(SeeTriggerTag)) {

                if(selection.gameObject.GetComponent<TriggerPhaseOneLook>() != null) {
                    selection.gameObject.GetComponent<TriggerPhaseOneLook>().triggerPhase();
                }
            }
        }

        //drop object that is picked up                       
        if(Input.GetButtonDown("Drop") && pickedUp){
            pickedUp = false;
            selected.parent = null;

            if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            }  else { 
                selected.gameObject.AddComponent<Rigidbody>(); 
            }
        } 
        else if(Input.GetButtonDown("Throw") && pickedUp) {
            pickedUp = false;
            selected.parent = null;

            if(selected.gameObject.GetComponent<Rigidbody>() != null) {
                selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                selected.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 502);

            }  else { 
                selected.gameObject.AddComponent<Rigidbody>(); 
                selected.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 502);
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }
}
