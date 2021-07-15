using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    string GameScene;

    [SerializeField] GameObject cutsceneObject;

    [SerializeField] GameObject background;
    [SerializeField] GameObject splashScreen;

    [SerializeField] Image cover;
    [SerializeField] GameObject loadingIcon;
    [SerializeField] GameObject skipText;

    WaitForEndOfFrame wait1Frame;

    void Awake()
    {
        wait1Frame = new WaitForEndOfFrame();

        if(GameManager.gameManager)
        {
            GameManager.gameManager.SelfDestruct();
        }

        FindObjectOfType<Toggle>().isOn = GameManager.debug;

        try{
        skipText.SetActive(false);
        loadingIcon.SetActive(false);
        }catch{
            print("SplashScreenManager (Awake) - gameObjects not found");
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartCutscene(){
        introCadiAnimationEvents.Instance.StartLoadingNextScene();
        cutsceneObject.SetActive(true);

        GetComponent<AudioSource>().enabled = true;

        loadingIcon.SetActive(true);
    }

    internal void displaySkip()
    {
        skipText.SetActive(true);
        loadingIcon.gameObject.SetActive(false);
    }

    public void swapMenu(GameObject nextMenu){
        nextMenu.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    //boooo it's not async go away
    public void StartGame() => SceneManager.LoadScene(GameScene);

    public void ToggleDebug() => GameManager.debug = FindObjectOfType<Toggle>().isOn;

    public void QuitGame() => Application.Quit(0);


    //for splash screen animator
    public void hideSplashScreen(){
        splashScreen.SetActive(false);
        background.SetActive(false);
        GameObject.Find("Main Menu Canvas").GetComponent<AudioSource>().enabled = true;
        this.gameObject.SetActive(false);
    }


}
