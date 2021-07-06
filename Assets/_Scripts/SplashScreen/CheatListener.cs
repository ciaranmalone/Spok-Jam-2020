using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheatListener : MonoBehaviour
{
    [SerializeField]
    GameObject notification;

    [Header("Debug Window")]
    [SerializeField] string debugWindow = "DEBUG";
    [SerializeField] GameObject debugCheckbox;
    Regex _debugWindow;




    List<char> input;

    // Start is called before the first frame update
    void Start()
    {
        notification.SetActive(false);
        processObjects();
        

        _debugWindow = processWord(debugWindow);
       
        input = new List<char>(16);
        for(int i = 0; i<input.Capacity; i++)
        {
            input.Add(' ');
        }
    }

    Regex processWord(string word)
    {
        char[] temp = word.ToUpper().ToCharArray();
        Array.Reverse(temp);
        word = new string(temp);
        return new Regex("^" + word);
    }

    void processObjects()
    {
        //set debug checkbox to whatever gamemanager's debug is
        debugCheckbox.SetActive(GameManager.debug);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && Input.inputString.Length!=0)
        {
            input.RemoveAt(input.Capacity - 1);
            input.Insert(0, Input.inputString.ToUpper()[0]);


            //print(new string(input.ToArray()));
            if(_debugWindow.Match(new string(input.ToArray())).Success)
            {
                //make debug appear
                debugCheckbox.SetActive(true);
                PromptNotification();
            }
        }
    }

    void PromptNotification()
    {
        notification.GetComponent<DeactivateTimer>().Prompt();
    }
}
