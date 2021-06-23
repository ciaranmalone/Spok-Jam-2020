using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ManagerInteraction : MonoBehaviour
{
    //Audio and subtitles
    [Header("Audio and Subtitles")]
    [SerializeField] Dialog[] dialog;
    [SerializeField] Dialog[] InteruptDialog;
    [SerializeField] private TextMeshProUGUI subTitletext;
    private AudioSource AudioSource;
    private bool playedAudio = false;
    
    //Movement
    [Header("Movement")]
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform endPoint;
    private NavMeshAgent agent;
    private int destPoint = 0;
    private GameObject Player;
    private bool left = false;

    //Animations
    [Header("Animations")]
    [SerializeField] managerAnimation managerAnim;
    [SerializeField] ManagerWheelAnimation wheelAnim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();
        subTitletext.text = "";
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) 
   {
       if(other.tag == "Player" && !playedAudio){
            StartCoroutine(DialogInteraction());
            managerAnim.setState(managerAnimation.state.talking);
            wheelAnim.setState(ManagerWheelAnimation.state.wheelSpin);

            GotoNextPoint();
            playedAudio = true;
            GameManager.gameManager.spawnNextTaskSheet();
       }
   }

    /// <summary>
    /// Plays through all the Dialog Clips and text in order then calls Leave Store IEnumerator
    /// </summary>
    IEnumerator DialogInteraction() {
        ProgrammaticQuests.PhaseID currentPhase = GameManager.gameManager.phase;

        for(int i = 0; i < dialog.Length; i++)
        {
            AudioSource.clip = dialog[i].dialogAudio;
            subTitletext.text = dialog[i].dialogText;
            AudioSource.Play();
          
            yield return new WaitForSeconds(AudioSource.clip.length);

            if (currentPhase != GameManager.gameManager.phase)
            {
                AudioSource.clip = InteruptDialog[0].dialogAudio;
                subTitletext.text = InteruptDialog[0].dialogText;
                AudioSource.Play();
                break;
            }
        }
        StartCoroutine(LeaveStore());    
    }

    void Update () {
        if (destPoint != 0 && destPoint < points.Length && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (agent.velocity == new Vector3(0,0,0)) 
        {
            transform.LookAt( new Vector3(Player.transform.position.x, 0, Player.transform.position.z));

        }

        if (destPoint == points.Length && !left) {
            wheelAnim.setState(ManagerWheelAnimation.state.idle);
            agent.isStopped = true;

        }
    }

    void GotoNextPoint() {
        if (points.Length == 0)
            return;
 
        agent.SetDestination(points[destPoint].position);
        destPoint = (destPoint + 1);
    }

    IEnumerator LeaveStore() {
        agent.isStopped = false;

        left = true;
        managerAnim.setState(managerAnimation.state.idle);
        wheelAnim.setState(ManagerWheelAnimation.state.wheelSpin);
        agent.SetDestination(endPoint.position);

        yield return new WaitForSeconds(10f);
        subTitletext.text = "";
        Destroy(gameObject);   
    } 
}

[System.Serializable]
internal class Dialog
{
    [SerializeField] internal string dialogText;
    [SerializeField] internal AudioClip dialogAudio;
}