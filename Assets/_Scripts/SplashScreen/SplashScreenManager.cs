using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    string GameScene;
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

    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ToggleDebug()
    {
        GameManager.debug = FindObjectOfType<Toggle>().isOn;
    } 

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
