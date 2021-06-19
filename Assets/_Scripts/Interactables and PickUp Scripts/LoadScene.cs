using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    Animator animator;
    [SerializeField] private string sceneName = "Lidl";
    [SerializeField] Vector3 areaToTeleport = new Vector3(-65, -21, -70);
    [SerializeField] bool setDestination = false;

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

        GameManager.gameManager.Teleport(sceneName,setDestination? Dumb3.Vector32Dumb3(areaToTeleport): null);
    }
}
