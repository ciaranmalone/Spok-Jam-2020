using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadonTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] Vector3 areaToTeleport =  new Vector3(-65,-21,-70);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gameManager.Teleport(sceneName, Dumb3.Vector32Dumb3(areaToTeleport));
        }
    }
}
