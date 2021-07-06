using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    string GameScene;

    [SerializeField] GameObject cutsceneObject;

    [SerializeField] GameObject background;
    [SerializeField] GameObject splashScreen;

    [SerializeField] Image cover;
    [SerializeField] GameObject skipText;
    void Awake()
    {
        if(GameManager.gameManager)
        {
            GameManager.gameManager.SelfDestruct();
        }
        FindObjectOfType<Toggle>().isOn = GameManager.debug;
        skipText.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartCutscene(){
        introCadiAnimationEvents.Instance.StartLoadingNextScene();
        cutsceneObject.SetActive(true);
        GetComponent<AudioSource>().enabled = true;
    }

    internal void displaySkip()
    {
        skipText.SetActive(true);
    }

    //boooo it's not async go away
    public void StartGame() => SceneManager.LoadScene(GameScene);

    public void ToggleDebug() => GameManager.debug = FindObjectOfType<Toggle>().isOn;

    public void QuitGame() => Application.Quit(0);


    //for splash screen animator
    public void hideSplashScreen(){
        splashScreen.SetActive(false);
        background.SetActive(false);
        GameObject.Find("Menu Canvas").GetComponent<AudioSource>().enabled = true;
        this.gameObject.SetActive(false);
    }


}
