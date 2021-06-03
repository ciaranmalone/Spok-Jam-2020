using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProgrammaticQuests;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    //Player
    private GameObject player;
    private Vector3 playerPos;
    private Quaternion playerRot;
    private Quaternion playerCameraRot;
    private bool playerTP;
    //private GameObject heldObject;

    //Quest System
    internal QuestSystem qs;
    /// <summary>
    /// Collection of World Phases
    /// </summary>
    internal WorldQuests.Phase[] phases;

    //Canvas
    GameCanvas canvas;

    //Game
    PhaseID phase = PhaseID.Phase1;
    int missionsRemaining;//TODO can be refactored to just use array below
    Dictionary<QuestID,bool> completedQuests;
    
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called when a scene has loaded
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Import current scene objects 
        player = GameObject.FindGameObjectWithTag("Player");
        qs = FindObjectOfType<QuestSystem>();
        phases = FindObjectsOfType<WorldQuests.Phase>();


        //If teleporting, do it now!!!
        if (playerTP && player)
        {
            player.transform.position = playerPos;
            player.GetComponentInChildren<MouseLook>().Rotate(playerCameraRot.eulerAngles.x, playerRot.eulerAngles.y);
        }

        //complete every quest before current phase
        for (PhaseID i = PhaseID.Phase1 ; i < phase; phase++)
        {
            GameObject iterPhase = GetCurrentPhase(phase);


            foreach (WorldQuests.Quest quest in iterPhase.transform.GetComponentsInChildren<WorldQuests.Quest>())
            {
                quest.completeQuest();
            }
        }

        //assign current phase if it doesn't exist
        if(completedQuests == null)
        {
            completedQuests = new Dictionary<QuestID, bool>();
            GameObject iterPhase = GetCurrentPhase(phase);
            

            foreach(WorldQuests.Quest quest in iterPhase.GetComponentsInChildren<WorldQuests.Quest>())
            {
                completedQuests.Add(quest.quest_id, false);
            }
        }
        //complete specified quests in current phase
        else
        {
            WorldQuests.Quest[] questlist = GetCurrentPhase(phase).GetComponentsInChildren<WorldQuests.Quest>();

            foreach(WorldQuests.Quest qst in questlist)
            {
                bool pass;
                if(completedQuests.TryGetValue(qst.quest_id, out pass))
                {
                    if(pass)
                    {
                        qst.completeQuest();
                    }
                }
            }
        }
    }

    internal void QuestComplete(QuestID quest_id)
    {
        completedQuests[quest_id] = true;
    }

    GameObject GetCurrentPhase(PhaseID phase)
    {
        //find the current phase
        foreach (WorldQuests.Phase iterPhase in phases)
        {
            if (iterPhase.GetComponent<WorldQuests.Phase>().phase == phase)
            {
                return iterPhase.gameObject;
            }
        }
        return null;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Teleport(SceneManager.GetActiveScene().name);
        }
    }


    /// <summary>
    /// Teleports the player to a different scene, currently:
    ///     - persisting the player's position and rotation 
    ///     - flexibility to change the position
    /// </summary>
    /// <param name="scene"></param>
    void Teleport(string scene, Dumb3 pos = null)
    {
        playerPos = pos==null ? player.transform.position : new Vector3(pos.x, pos.y, pos.z);
        playerRot = player.transform.rotation;
        playerCameraRot = player.GetComponentInChildren<Camera>().transform.localRotation;
        playerTP = true;
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// template on how to remove pesistance
    /// </summary>
    void Death()
    {
        playerTP = false;
    }






    void DrawQuests()
    {
        var parames = "";
        canvas.doTheThing(parames);
    }
}

internal class Dumb3
{
    internal float x, y, z;
    internal static readonly Dumb3 zero = new Dumb3(0, 0, 0);

    internal Dumb3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}