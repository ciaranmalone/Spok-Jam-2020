using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private List<sceneTeleport> scenes;
    private List<sceneTeleport> canTravel = new List<sceneTeleport>();
    private int indexChoice;
    private Animator animator;
    private AIAudioController audioController;

    private void Start()
    {
        Debug.Log($"Scenes Before: {scenes.Count}");

        //adds scene to canTravel List if the Token has not been collected
        foreach (var scene in scenes)
        {
            if (!GameManager.gameManager.susTokens[(int)scene.token])
                canTravel.Add(scene);
        }

        Debug.Log($"Scenes after: {canTravel.Count}");

        audioController = GetComponent<AIAudioController>();
        animator = IndicatorSingletons.blackScreenSingleton.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("loadTheScene");
        }
    }
    private IEnumerator loadTheScene()
    {
        audioController.Caught();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        indexChoice = Random.Range(0, canTravel.Count);
        if (canTravel[indexChoice].specificLocation)
            GameManager.gameManager.Teleport(canTravel[indexChoice].sceneName, Dumb3.Vector32Dumb3(canTravel[indexChoice].playerTeleportLocation));
        else
            GameManager.gameManager.Teleport(canTravel[indexChoice].sceneName);
    }
}

[System.Serializable]
internal class sceneTeleport
{
    [SerializeField] internal string sceneName;
    [SerializeField] internal bool specificLocation = false;
    [SerializeField] internal Vector3 playerTeleportLocation;
    [SerializeField] internal SusToken.Token token;
}
