using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "Lidl";
    public void LoadSpecifiedScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
