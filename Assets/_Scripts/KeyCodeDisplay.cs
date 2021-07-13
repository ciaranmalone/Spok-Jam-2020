using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyCodeDisplay : MonoBehaviour
{

    TextMeshPro tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.text = "";
        for(int i = 0; i < GameManager.gameManager.keyPadCode.Length; i++)
        {
            tmp.text += GameManager.gameManager.keyPadCode[i];
        }
    }
}
