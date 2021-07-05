using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introCadiAnimationEvents : MonoBehaviour
{
    [SerializeField] Image UICover;

    [SerializeField] string nextScene = "Lidl";

    bool skippable = false;

    public static introCadiAnimationEvents Instance;

    void Awake() => Instance = this;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && skippable)
            endCutscene();
    }

    public void canSkip() => skippable = true;
    

    public void endCutscene()=> SceneManager.LoadSceneAsync("Lidl");
}
