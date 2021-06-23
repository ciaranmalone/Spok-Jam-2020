using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProgrammaticQuests;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static bool debug = false;

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
    Dictionary<PhaseID, Dictionary<QuestID, bool>> bonusQuests;
    internal bool looped = false;
    
    /// <summary>
    /// to check if game is loading
    /// </summary>
    internal bool loading;
    /// <summary>
    /// for fail safe when trying to go to the main menu
    /// </summary>
    bool pressedEscape = false;

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
        for (PhaseID i = PhaseID.Phase1 ; i < phase; i++) //EXTREMELY UNSTABLE, PROCEED WITH CAUTION, ask me if you want to know why (p0)
        {
            GameObject iterPhase = GetCurrentPhase(i);


            foreach (WorldQuests.Quest quest in iterPhase.transform.GetComponentsInChildren<WorldQuests.Quest>())
            {
                quest.CallPostQuestEvent();
            }
        }

        //create bonus quest dictionary if doesn't exists
        if (bonusQuests == null) bonusQuests = new Dictionary<PhaseID, Dictionary<QuestID, bool>>();

        //complete specified quests in current phase
        if(completedQuests!=null && completedQuests.Count>0)
        {
            RebuildQuestList();
        }
        //assign current phase if it doesn't exist
        else
        {
            CreatePhase();
        }


        SetActivePhase();
        loading = false;
    }

    internal void RebuildQuestList(bool visual = false)
    {
        //draw current phase quests
        DrawQuests();
        if (!visual)
        {
            WorldQuests.Quest[] questlist = GetCurrentPhase(phase).GetComponentsInChildren<WorldQuests.Quest>();

            foreach (WorldQuests.Quest qst in questlist)
            {
                bool pass;
                if (completedQuests.TryGetValue(qst.quest_id, out pass))
                {
                    if (pass)
                    {
                        qst.QuestCompleteWQ();
                    }
                }
            }

            //handle bonus quests
            WorldQuests.EggQuest[] bonusQuestlist = FindObjectsOfType<WorldQuests.EggQuest>();
            Dictionary<QuestID, bool> bonus;

            //get current phase's bonus quests
            bonusQuests.TryGetValue(phase, out bonus);
            if (bonus != null)
            {
                foreach (WorldQuests.EggQuest qst in bonusQuestlist)
                {
                    //if the quest exists in current phase
                    if (bonus.ContainsKey(qst.quest_id))
                    {
                        //complete quest if exists
                        if (bonus[qst.quest_id] == true)
                        {
                            QuestCompleteGM(qst.quest_id);
                        }
                    }
                }
            }
        }
        else
        {
            foreach (KeyValuePair<QuestID, bool> qst in completedQuests)
            {
                //if passed
                if (qst.Value) canvas.QuestCompleteC(getQuestPosition(qst.Key));
            }
            Dictionary<QuestID, bool> bonus;
            if (bonusQuests.TryGetValue(phase, out bonus))
            {
                foreach (KeyValuePair<QuestID, bool> qst in bonus)
                {
                    //if passed
                    if (qst.Value) canvas.QuestCompleteC(getQuestPosition(qst.Key));
                }
            }
        }
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

        if (iterPhase)
        {
            foreach (WorldQuests.Quest quest in iterPhase.GetComponentsInChildren<WorldQuests.Quest>())
            {
                completedQuests.Add(quest.quest_id, false);
            }
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

    internal void CreateBonusQuest(QuestID quest_id)
    {
        //get current phase's set of bonus quests
        Dictionary<QuestID, bool> currPhase;
        bonusQuests.TryGetValue(phase, out currPhase);

        //if it doesn't exist, create one
        if(currPhase == null)
        {
            bonusQuests.Add(phase, new Dictionary<QuestID, bool>());
            bonusQuests.TryGetValue(phase, out currPhase);
        }

        //if it doesn't already exist
        if (!currPhase.ContainsKey(quest_id))
        {
            //add the quest
            currPhase.Add(quest_id, false);

            //draw the quest
            canvas.AddQuestC(quest_id);

            //increase missions remaining for current phase
            missionsRemaining++;

            //redraw quests to adjust for scale
            RebuildQuestList(true);
        }
    }

    /// <summary>
    /// Finds the position of the quest in the current phase
    /// </summary>
    /// <returns>position of the completed quests, or -1 if it doesn't exist</returns>
    internal int getQuestPosition(QuestID quest_id)
    {
        int whereAmI = 0;
        //main quests
        foreach (KeyValuePair<QuestID, bool> q in completedQuests)
        {
            if (q.Key == quest_id) return whereAmI;
            whereAmI++;
        }

        //bonus quests
        Dictionary<QuestID, bool> bonus;
        bonusQuests.TryGetValue(phase, out bonus);
        if (bonus != null)
        {
            foreach (KeyValuePair<QuestID, bool> q in bonus)
            {
                if (q.Key == quest_id) return whereAmI;
                whereAmI++;
            }
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
        if(completedQuests.ContainsKey(quest_id)) completedQuests[quest_id] = true;
        else
        {
            Dictionary<QuestID, bool> bonus;
            bonusQuests.TryGetValue(phase, out bonus);
            //if (bonus == null) return;
            if (bonus.ContainsKey(quest_id)) bonus[quest_id] = true;
            else return;
        }
        QuestUpdateGM(quest_id, 0, true);
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
                string curr = $"phoneCall{(int)phase-1}";
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
    internal void QuestUpdateGM(QuestID quest_id, int howManyCompleted, bool full = false)
    {
        Quest quest = qs.getQuest(quest_id);
        if (full) howManyCompleted = quest.count;
        canvas.QuestUpdateC(quest, getQuestPosition(quest_id), howManyCompleted);
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
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Teleport(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartPhaseLoop();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                CreatePhase(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pressedEscape)
            {
                SceneManager.LoadScene("Splash");
            }

            pressedEscape = true;
        }
        else if(Input.anyKeyDown)
        {
            pressedEscape = false;
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

        GameObject temp = player.GetComponentInChildren<SelectItem>().getHeldObject();
        if (temp)
        {
            heldObjectCache = Instantiate(temp);
            heldObjectCache.SetActive(false);
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
        Dictionary<QuestID, bool> bonus;
        bonusQuests.TryGetValue(phase, out bonus);
        if (bonus == null) bonus = new Dictionary<QuestID, bool>();
        QuestID[] quests = new QuestID[completedQuests.Keys.Count + bonus.Keys.Count];
        completedQuests.Keys.ToArray().CopyTo(quests, 0);
        bonus.Keys.ToArray().CopyTo(quests, completedQuests.Keys.Count);
        canvas.MakeObjectives(quests, quests.Length < 6 ? 1 : quests.Length < 12 ? 2 : 3);
    }

    internal bool isQuestComplete(QuestID quest)
    {
        bool didI;
        if(completedQuests.TryGetValue(quest, out didI)) return didI;
        return false;
    }

    internal void StartPhaseLoop()
    {
        phase = PhaseID.Phase0;
        looped = true;

        ///NOT NEEDED UNLESS I REFACTOR ONSCENELOADED TO ACTUALLY WORRY ABOUT THE PHASE 0!!!
        //GameObject funky = Instantiate(new GameObject(), new Vector3(player.transform.position.x, player.transform.position.y + 399, player.transform.position.z), Quaternion.identity, //bad transform, do p0 instead player.transform);
        //funky.AddComponent<WorldQuests.Quest>().quest_id = QuestID.P0M0;
        //funky.AddComponent<BoxCollider>().isTrigger = true;

        SetActivePhase();
        CreatePhase();
    }

    private void OnDestroy()
    {
        //no need to worry about unloading this way?
        if(this == gameManager)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            gameManager = null;
        }
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

    /// <summary>
    /// Funniest shit I ever seen
    /// </summary>
    /// <param name="pos">where we droppin</param>
    /// <returns>Turned himself into a Dumb3</returns>
    internal static Dumb3 Vector32Dumb3(Vector3 pos) => new Dumb3(pos.x, pos.y, pos.z);
}