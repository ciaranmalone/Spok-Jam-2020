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

    void Awake()
    {
        if(GameManager.gameManager)
        {
            GameManager.gameManager.SelfDestruct();
        }
        FindObjectOfType<Toggle>().isOn = GameManager.debug;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartCutscene(){
        cutsceneObject.SetActive(true);
        GetComponent<AudioSource>().enabled = true;
    }

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
