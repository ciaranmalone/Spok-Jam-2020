using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSingletons : MonoBehaviour
{

    [SerializeField] private GameObject pickUpIndicator;
    [SerializeField] private GameObject interactIndicator;
    [SerializeField] private GameObject crouchIndicator;
    [SerializeField] private GameObject sprintIndicator;
    
    
    public static GameObject pickupIndicatorSingleton;
    public static GameObject interactIndicatorSingleton;
    public static GameObject crouchIndicatorSingleton;
    public static GameObject sprintIndicatorSingleton;
    
    void Awake()
    {
        pickupIndicatorSingleton = pickUpIndicator;
        interactIndicatorSingleton = interactIndicator;
        crouchIndicatorSingleton = crouchIndicator;
        sprintIndicatorSingleton = sprintIndicator;
    }
}
