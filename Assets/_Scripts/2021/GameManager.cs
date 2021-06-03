using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private QuestSystem qs;

    //Canvas

    //Game
    int phase;
    int missionsRemaining;
    
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
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        qs = FindObjectOfType<QuestSystem>();

        if (playerTP)
        {
            player.transform.position = playerPos;
            player.GetComponentInChildren<MouseLook>().Rotate(playerCameraRot.eulerAngles.x, playerRot.eulerAngles.y);
        }
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