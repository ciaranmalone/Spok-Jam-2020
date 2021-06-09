using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollisonTrigger : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    private int indexChoice;
    private Animator animator;

    private void Start()
    {
        animator = IndicatorSingletons.blackScreenSingleton.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" )
        {
            StartCoroutine("loadTheScene");
        }
    }
    private IEnumerator loadTheScene()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        indexChoice = Random.Range(0, scenes.Length);
        GameManager.gameManager.Teleport(scenes[indexChoice]);
    }
}