using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introCadiAnimationEvents : MonoBehaviour
{
    [SerializeField] Image UICover;

    [SerializeField] string nextScene = "Lidl";

    [SerializeField] SplashScreenManager masterSplashScreenManager;

    public static introCadiAnimationEvents Instance;

    bool cutsceneEnded = false;

    void Awake() => Instance = this;
    
    public void StartLoadingNextScene() => StartCoroutine("NextSceneCoroutine");

    public void endCutscene()
    {
        cutsceneEnded = true;
    }

    public IEnumerator NextSceneCoroutine()
    {
        yield return null; //making it smooth
        
        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        loading.allowSceneActivation = false;

        bool done = false; // if it's 90% loaded
        float timer = 0; //scene loads too fast so space things appears too fast >.<

        //wait until it's done
        while (!loading.isDone)
        {
            timer += !done ? Time.deltaTime : 0;

            if(!done && loading.progress >= 0.9f && timer > 5)
            {
                //when it's done kindly ask the splash screen manager to display the skip
                masterSplashScreenManager.displaySkip();
                done = true;
            }
            
            //wait until player yoinks space when it's done
            if (done && Input.GetKeyDown(KeyCode.Space))
            {
                //start fading to black
                loading.allowSceneActivation = true;
            }

            if (cutsceneEnded) // || some other thing (not transparent bg?)
                loading.allowSceneActivation = true;

            yield return null;
        }
    }
}
