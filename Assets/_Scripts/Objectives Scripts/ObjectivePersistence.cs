using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivePersistence : MonoBehaviour
{
    public static ObjectivePersistence Instance;

    private WaitForEndOfFrame waitOneFrame = new WaitForEndOfFrame();
    
    [SerializeField] ObjectivePersistenceBoolArray[] Objectives;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        StartCoroutine(markCompletedTasks());
    }

    IEnumerator markCompletedTasks()
    {
        //wait a frame for game manager to catch up
        yield return waitOneFrame;
        
        //cycle through each array of objective bools
        for (int i = 1; i <= Objectives.Length; i++)
        {
            //find all TriggerCompleteObjective scripts under the MissionPhase Object corresponding with the current array.
            TriggerCompleteObjective[] objectivesInCurrentList = GameObject.Find("MissionPhase" + i.ToString())
                .GetComponentsInChildren<TriggerCompleteObjective>();
            //cycle through these scripts
            for (int o = 0; o <= objectivesInCurrentList.Length-1; o++)
            {
                try
                {
                    /*
                     * THIS ASSUMES THE OBJECTIVE SCRIPTS IN EDITOR ARE IN ORDER
                     * AND CORRESPOND WITH THE ORDER OF OBJECTIVE BOOLS IN THIS SCRIPT
                     */ 
                    
                    //if the bool on this script for the current TriggerCompleteObjective is true
                    //complete the task
                    if (Objectives[i - 1].Objectives[o])
                    {
                        objectivesInCurrentList[o].completeObjective();
                    }
                    
                    /*
                     * PROGRESSION WITH THIS SCRIPT CANNOT CONTINUE UNTIL THE SPAWNING OF TASK SHEETS
                     * IS MOVED FROM THE MANAGER'S TRIGGER AND PHONE INTERACTION SCRIPT.
                     */
                }
                catch
                {
                    Debug.Log("Error in objective persistence loop.\n o:" + o + " i:" +i);
                }

                yield return waitOneFrame;
            }
        }
    }
    
    void Start()
    {
        //Singleton
        if(!Instance) Instance = this;
        else Destroy(this);
    }


}


/*
    Unity can't display 2D arrays in editor.
    But it can display an array of classes 
    that can themselves contain serialized arrays.

    This is stupid.
    But necessary.
*/
[System.Serializable]
class ObjectivePersistenceBoolArray{
    public bool[] Objectives;
}
