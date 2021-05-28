using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gamePrefabs;
    
    private static List<GameObject> initializedGamePrefabs; 
    void Start()
    {
        if(initializedGamePrefabs == null)
        {
            initializedGamePrefabs = new List<GameObject>();

            foreach (GameObject item in gamePrefabs)
            {
                initializedGamePrefabs.Add(Instantiate(item));
            }
        }
    }
}