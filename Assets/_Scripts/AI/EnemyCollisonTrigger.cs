using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    private int indexChoice;

    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" )
        {
            indexChoice = Random.Range (0, scenes.Length);
            SceneManager.LoadSceneAsync(scenes[indexChoice]);
        }
    }
}