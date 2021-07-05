using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private sceneTeleport[] scenes;
    private int indexChoice;
    private Animator animator;
    private AIAudioController audioController;

    private void Start()
    {
        audioController = GetComponent<AIAudioController>();
        animator = IndicatorSingletons.blackScreenSingleton.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") )
        {
            StartCoroutine("loadTheScene");
        }
    }
    private IEnumerator loadTheScene()
    {   
        audioController.Caught();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        indexChoice = Random.Range(0, scenes.Length);
        if (scenes[indexChoice].specificLocation)
            GameManager.gameManager.Teleport(scenes[indexChoice].sceneName, Dumb3.Vector32Dumb3(scenes[indexChoice].playerTeleportLocation)); 
        else 
            GameManager.gameManager.Teleport(scenes[indexChoice].sceneName);
    }
}

[System.Serializable]
internal class sceneTeleport
{
    [SerializeField] internal string sceneName;
    [SerializeField] internal bool specificLocation = false;
    [SerializeField] internal Vector3 playerTeleportLocation;
}
