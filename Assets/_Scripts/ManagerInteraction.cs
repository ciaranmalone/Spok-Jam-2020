using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ManagerInteraction : MonoBehaviour
{
    //Audio and subtitles
    [Header("Audio and Subtitles")]
    [SerializeField] private AudioClip[] AudioClips;
    [SerializeField] private string[] subtitles;
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
            StartCoroutine(dialBegin());
            managerAnim.setState(managerAnimation.state.talking);
            wheelAnim.setState(ManagerWheelAnimation.state.wheelSpin);

            GotoNextPoint();
            playedAudio = true;
            GameEvents.current.spawnNextNote();
       }
   }

    IEnumerator dialBegin() {
        for(int i = 0; i < AudioClips.Length; i++)
        {
            AudioSource.clip = AudioClips[i];
            subTitletext.text = subtitles[i];
            AudioSource.Play();
            yield return new WaitForSeconds(AudioSource.clip.length);   
        }
        subTitletext.text = "";
        StartCoroutine(leaveStore());    
    }

    void Update () {
        if (destPoint != 0 && destPoint < points.Length && !agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if(agent.velocity == new Vector3(0,0,0)) {
            transform.LookAt( new Vector3(Player.transform.position.x, 0, Player.transform.position.z));
           //wheelAnim.setState(ManagerWheelAnimation.state.idle);
        }

        if(destPoint ==  points.Length && !left) {
            wheelAnim.setState(ManagerWheelAnimation.state.idle);
        }
    }

    private void FixedUpdate() {
        
    }
    void GotoNextPoint() {
        if (points.Length == 0)
            return;
 
        agent.SetDestination(points[destPoint].position);
        destPoint = (destPoint + 1);
    }

    IEnumerator leaveStore() {
        left = true;
        managerAnim.setState(managerAnimation.state.idle);
        wheelAnim.setState(ManagerWheelAnimation.state.wheelSpin);
        agent.SetDestination(endPoint.position);

        yield return new WaitForSeconds(10f);   
        Destroy(gameObject);   
    } 
}
