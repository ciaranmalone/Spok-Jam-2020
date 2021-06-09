using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    Animator animator;
    [SerializeField] private string sceneName = "Lidl";

    private void Start()
    {
        animator = IndicatorSingletons.blackScreenSingleton.GetComponent<Animator>();
    }
    public void StartTransition()
    {
        StartCoroutine("loadTheScene");
    }

    private IEnumerator loadTheScene()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.Teleport(sceneName);
    }
}
