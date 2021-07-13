using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RealAnimation))]
public class KeyPad : MonoBehaviour
{
    [SerializeField]
    GameObject[] numbers;
    [SerializeField]
    TextMeshPro display;

    int[] inputtedCode;
    bool passed = false;
    int currPos = 0;


    private void Start()
    {
        inputtedCode = new int[4];
    }

    internal void Interact(GameObject number)
    {
        int num = Array.IndexOf(numbers, number);
        if (num == -1 || passed) return;
        if (currPos == 0)
        {
            display.text = "";
            display.color = Color.white;
        }

        inputtedCode[currPos] = num;
        display.text += inputtedCode[currPos];

        if (currPos==3)
        {
            
            if (verifyCode())
            {
                display.color = Color.green;
                passed = true;
                GetComponent<RealAnimation>().Animate();
            }
            else
            {
                currPos = 0;
                display.color = Color.red;
            }
        }
        else
        {
            currPos++;
        }
    }

    bool verifyCode()
    {
        for (int i = 0; i < inputtedCode.Length; i++)
        {
            if (inputtedCode[i] != GameManager.gameManager.keyPadCode[i]) return false;
        }
        return true;
    }
}
