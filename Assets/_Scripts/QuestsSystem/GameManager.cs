using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProgrammaticQuests;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    //Player
    private GameObject player;
    private Vector3 playerPos;
    private Quaternion playerRot;
    private Quaternion playerCameraRot;
    private bool playerTP;
    private GameObject heldObjectCache;

    //private GameObject heldObject;

    //Quest System
    internal QuestSystem qs;
    /// <summary>
    /// Collection of Phases in the Unity Hierarchy
    /// </summary>
    internal WorldQuests.Phase[] phases;
    internal AudioSource[] speakers;
    internal phoneCallScript[] phones;

    //Canvas
    GameCanvas canvas;

    //Game
    internal PhaseID phase = PhaseID.Phase1;
    int missionsRemaining;//TODO can be refactored to just use array below
    Dictionary<QuestID, bool> completedQuests;
    /// <summary>
    /// to check if game is loading
    /// </summary>
    internal bool loading; 
    
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    internal void spawnNextTaskSheet()
    {
        foreach (WorldQuests.Phase ph in phases)
        {
            if(ph.phase==phase) ph.gameObject.transform.Find("TaskSheet").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Called when a scene has loaded
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loading = true;

        //Import current scene objects 
        player = GameObject.FindGameObjectWithTag("Player");
        qs = FindObjectOfType<QuestSystem>();
        phases = FindObjectsOfType<WorldQuests.Phase>();
        canvas = FindObjectOfType<GameCanvas>();
        phones = FindObjectsOfType<phoneCallScript>();
        
        ///subcomment for importing speakers
        ///
        List<AudioSource> spkr_temp = new List<AudioSource>();
        foreach(GameObject spkr in GameObject.FindGameObjectsWithTag("speaker"))
        {
            spkr_temp.Add(spkr.GetComponent<AudioSource>());
        }
        speakers = spkr_temp.ToArray();


        //If teleporting, do it now!!!
        if (playerTP && player)
        {
            player.transform.position = playerPos;
            float xRot = playerCameraRot.eulerAngles.x;
            player.GetComponentInChildren<MouseLook>().Rotate(xRot > 90 ? xRot-360 : xRot, playerRot.eulerAngles.y);

            //check for held
            if(heldObjectCache)
            {
                heldObjectCache.SetActive(true);
                player.GetComponentInChildren<SelectItem>().PickUpObject(Instantiate(heldObjectCache).transform);
                Destroy(heldObjectCache);
            }

        }

        //complete every quest before current phase
        for (PhaseID i = PhaseID.Phase1 ; i < phase; i++)
        {
            GameObject iterPhase = GetCurrentPhase(i);


            foreach (WorldQuests.Quest quest in iterPhase.transform.GetComponentsInChildren<WorldQuests.Quest>())
            {
                quest.CallPostQuestEvent();
            }
        }

        //complete specified quests in current phase
        if(completedQuests!=null)
        {
            //draw current phase quests
            DrawQuests();

            WorldQuests.Quest[] questlist = GetCurrentPhase(phase).GetComponentsInChildren<WorldQuests.Quest>();

            foreach(WorldQuests.Quest qst in questlist)
            {
                bool pass;
                if(completedQuests.TryGetValue(qst.quest_id, out pass))
                {
                    if(pass)
                    {
                        qst.QuestCompleteWQ();
                    }
                }
            }
        }
        //assign current phase if it doesn't exist
        else
        {
            CreatePhase();
        }


        SetActivePhase();
        loading = false;
    }

    internal void CreatePhase(bool _new = false)
    {
        if (_new)
        {
            phase++;
            SetActivePhase();
        }
        completedQuests = new Dictionary<QuestID, bool>();
        GameObject iterPhase = GetCurrentPhase(phase);

        foreach (WorldQuests.Quest quest in iterPhase.GetComponentsInChildren<WorldQuests.Quest>())
        {
            completedQuests.Add(quest.quest_id, false);
        }
        missionsRemaining = completedQuests.Count;

        //draw current phase quests
        DrawQuests();
    }

    void SetActivePhase()
    {
        //hide every unused phase
        foreach (WorldQuests.Phase p in phases)
        {
            if (p.phase != phase) p.gameObject.SetActive(false);
            else { p.gameObject.SetActive(true); }
        }
    }

    /// <summary>
    /// Finds the position of the quest in the current phase
    /// </summary>
    /// <returns>position of the completed quests, or -1 if it doesn't exist</returns>
    internal int getQuestPosition(QuestID quest_id)
    {
        int whereAmI = 0;
        foreach (KeyValuePair<QuestID, bool> q in completedQuests)
        {
            if (q.Key == quest_id) return whereAmI;
            whereAmI++;
        }
        return -1;
    }

    /// <summary>
    /// GameManager-side of completeting quests:
    ///  - local phase completion tracker gets updated
    ///  - updates the canvas
    ///  - plays the sound
    ///  
    /// could be possibly refactored to get the quest rather than the questid
    /// </summary>
    /// <param name="quest_id"></param>
    internal void QuestCompleteGM(QuestID quest_id)
    {
        completedQuests[quest_id] = true;
        canvas.QuestCompleteC(getQuestPosition(quest_id));
        
        //if it is not loading then u can do some stuff here
        if (!loading)
        {
            AudioClip clip = qs.getQuest(quest_id).clip;
            if (clip)
            {
                foreach (AudioSource c in speakers)
                {
                    try
                    {
                        c.PlayOneShot(clip);
                    }
                    catch
                    {
                        try
                        {
                            print(c.gameObject);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e.Message);
                        }
                    }
                }
            }

            missionsRemaining--;
        }
        if (missionsRemaining == 0)
        {
            foreach (phoneCallScript phone in phones)
            {
                string curr = $"phoneCall{(int)phase}";
                if (phone.phase == curr)
                {
                    phone.PhasePhoneCall(curr);
                }
            }
        }
    }

    /// <summary>
    /// Update the counter or the name of a quest, GameManager side
    /// </summary>
    /// <param name="quest_id">The id of the quest</param>
    /// <param name="howManyCompleted">How much is completed</param>
    internal void QuestUpdateGM(QuestID quest_id, int howManyCompleted)
    {
        canvas.QuestUpdateC(qs.getQuest(quest_id), getQuestPosition(quest_id), howManyCompleted);
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
    internal void Teleport(string scene, Dumb3 pos = null)
    {
        playerPos = pos==null ? player.transform.position : new Vector3(pos.x, pos.y, pos.z);
        playerRot = player.transform.rotation;
        playerCameraRot = player.GetComponentInChildren<Camera>().transform.localRotation;
        playerTP = true;

        heldObjectCache = Instantiate(player.GetComponentInChildren<SelectItem>().getHeldObject());
        heldObjectCache.SetActive(false);
        if (heldObjectCache) //if holding an object
        {
            DontDestroyOnLoad(heldObjectCache);
        }
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
        canvas.MakeObjectives(completedQuests.Keys);
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