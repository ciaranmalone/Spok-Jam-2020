using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!pauseCanvas.activeInHierarchy){
                Time.timeScale = 0;
                GameManager.gameManager.gameCanvasGameObject.SetActive(false);
                pauseCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                resume();
            }
        }
    }

    public void resume(){
        Time.timeScale = 1;
        GameManager.gameManager.gameCanvasGameObject.SetActive(true);
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quitToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void swapMenu(GameObject nextMenu){
        nextMenu.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }
}
