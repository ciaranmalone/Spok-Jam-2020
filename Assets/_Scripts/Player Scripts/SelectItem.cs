﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private string PickUpTag = "pickUp";
    [SerializeField] private string InteractableTag = "interactable";
    [SerializeField] private string SeeTriggerTag = "lookAtMe";
    [SerializeField] private float throwForce = 1f;

    [SerializeField] AudioClip[] throwGrunts;
    private AudioSource theGrunter;

    [Header("Objects to display")]
    [SerializeField] private GameObject lightPointPickUp;
    [SerializeField] private GameObject lightPointInteract;

    private GameObject PickUpIndicator;
    private GameObject InteractIndicator;

    [Header("Input Controls")]
    [SerializeField] private KeyCode selectObject = KeyCode.E;
    [SerializeField] private KeyCode dropObject = KeyCode.Mouse1;
    [SerializeField] private KeyCode throwObject = KeyCode.Mouse0;

    [SerializeField] private Vector3 objectPosition = new Vector3(0, -1, 2);

    public static bool pickedUp = false;

    private Transform selection;
    private Transform interactTransform;
    private Transform pickedUpTransform;
    private bool imBeingLookedAtExists = true;
    private bool ObjectLookAtEventRan = false;

    private KeyPad kp;

    private void Start()
    {
        PickUpIndicator = IndicatorSingletons.pickupIndicatorSingleton;
        InteractIndicator = IndicatorSingletons.interactIndicatorSingleton;

        try
        {
            theGrunter = GetComponent<AudioSource>();
        }
        catch
        {
            theGrunter = gameObject.AddComponent<AudioSource>();
        }

        kp = FindObjectOfType<KeyPad>();
    }

    void Update()
    {
        RaycastHit hit;
        lightPointPickUp.SetActive(false);
        PickUpIndicator.SetActive(false);
        lightPointInteract.SetActive(false);
        InteractIndicator.SetActive(false);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f, ~IgnoreMe))
        {
            if (selection != hit.transform)
            {
                selection = hit.transform;
                imBeingLookedAtExists = true;
            }

            /**
             * checks if the item the raycast hit is in reach and if its tag is pickupable 
             * ie. can the player pick it up?
             */
            if (selection.CompareTag(PickUpTag) && !pickedUp && hit.distance < 5f)
            {

                lightPointPickUp.SetActive(true);
                PickUpIndicator.SetActive(true);
                lightPointPickUp.transform.position = hit.point;

                if (!ObjectLookAtEventRan)
                {
                    try
                    {
                        selection.gameObject.GetComponent<ObjectLookAtEvents>().ObjectLookAtEvent.Invoke();
                        ObjectLookAtEventRan = true;
                    }
                    catch
                    {
                        ObjectLookAtEventRan = true;
                    }
                }

                if (Input.GetKey(selectObject))
                {
                    PickUpObject(selection);
                }
            }
            /**
             * checks if the item the raycast hit is in reach and if its tag is interactable
             */
            else if (selection.CompareTag(InteractableTag) && hit.distance < 5f)
            {
                lightPointInteract.SetActive(true);
                InteractIndicator.SetActive(true);

                lightPointInteract.transform.position = hit.point;

                if (Input.GetKeyDown(selectObject))
                {
                    interactTransform = selection;
                    /**
                     * seeing if the the hit object has interactable else do nothing
                     * handle Interaction will play an animation on the selected item (ie. open door)
                     */
                    try { selection.gameObject.GetComponent<ObjectPickUpEvents>().ObjectPickUpEvent.Invoke(); } catch { }
                    try
                    {
                        interactTransform.gameObject.GetComponent<interactable>().handleInteraction();
                    }
                    catch { }
                    if (kp) kp.Interact(selection.gameObject);
                    interactTransform = null;
                }

            }
            else if (selection.CompareTag(SeeTriggerTag))
            {

                try { selection.gameObject.GetComponent<TriggerPhaseOneLook>().triggerPhase(); } catch { }
            }
        }

        /**
         * User Input checks for drop and throw
         */

        //drop object that is picked up                       
        if (Input.GetKey(dropObject) && pickedUp)
        {
            DropObject();
        }

        //throw object that is picked up              
        else if (Input.GetKey(throwObject) && pickedUp)
        {
            ThrowObject();
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    private void DropObject()
    {
        try { pickedUpTransform.gameObject.GetComponent<ObjectThrowEvents>().ObjectThrowEvent.Invoke(); } catch { }

        pickedUp = false;
        pickedUpTransform.parent = null;

        /*
         * this try and catch is just to make sure the object has a rigidbody, 
         * else if it doesn't add one for when it drops
         */
        try
        {
            Rigidbody selectedRB = pickedUpTransform.gameObject.GetComponent<Rigidbody>();
            selectedRB.isKinematic = false;
        }
        catch
        {
            pickedUpTransform.gameObject.AddComponent<Rigidbody>();
        }

        pickedUpTransform.gameObject.GetComponent<Collider>().enabled = true;
        pickedUpTransform = null;
    }


    private void ThrowObject()
    {
        try { pickedUpTransform.gameObject.GetComponent<ObjectThrowEvents>().ObjectThrowEvent.Invoke(); } catch { }

        pickedUp = false;
        pickedUpTransform.parent = null;
        /*
         * this try and catch is just to make sure the object has a rigidbody, 
         * else if it doesn't add one for when it thrown
         */
        try
        {
            Rigidbody selectedRB = pickedUpTransform.gameObject.GetComponent<Rigidbody>();
            selectedRB.isKinematic = false;
            selectedRB.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
        catch
        {
            Rigidbody selectedRB = pickedUpTransform.gameObject.AddComponent<Rigidbody>();
            selectedRB.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }

        if (throwGrunts.Length > 0) theGrunter.PlayOneShot(throwGrunts[Random.Range(0, throwGrunts.Length)]);

        pickedUpTransform.gameObject.GetComponent<Collider>().enabled = true;
        pickedUpTransform = null;
    }

    internal void PickUpObject(Transform obj)
    {
        selection = obj;
        pickedUpTransform = obj;
        pickedUp = true;
    
        //make object the child of the player
        pickedUpTransform.parent = gameObject.transform;
        pickedUpTransform.localPosition = objectPosition;
        pickedUpTransform.localRotation = Quaternion.identity;
        pickedUpTransform.gameObject.GetComponent<Collider>().enabled = false;

        //stop its phyiscs so it will stop giggling around
        try { pickedUpTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true; } catch { }
        try { selection.gameObject.GetComponent<ObjectPickUpEvents>().ObjectPickUpEvent.Invoke(); } catch { }
    }

    internal GameObject getHeldObject()
    {
        return pickedUpTransform ? pickedUpTransform.gameObject : null;
    }

    internal GameObject getLookedAtObject()
    {
        return selection ? selection.gameObject : null;
    }

}