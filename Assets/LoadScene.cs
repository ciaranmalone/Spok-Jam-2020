using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private string sceneName = "Lidl";
    public void StartTransition()
    {
        StartCoroutine("loadTheScene");
    }

    private IEnumerator loadTheScene()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(sceneName);

    }

}
