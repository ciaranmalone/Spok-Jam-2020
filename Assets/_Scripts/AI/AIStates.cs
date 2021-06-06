using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIStates : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [Header("States")] 
    [SerializeField] private aiState startingState;
    [SerializeField] private aiState currentState;
    private enum aiState {patrol, chase, attack, idle, waitingBetweenPatrolPoints};
    
    
    
    [Header ("Patrol")]
    [SerializeField] private float patrolMoveSpeed;
    [SerializeField] private int waypointGroupIndex = 0;
    [SerializeField] private AIWaypointGroup[] aiWaypointGroups;
    private List<Transform> patrolWaypoints;
    private int currentPatrolPointIndex;
    
    [Header ("Search")]
    [SerializeField] private bool searchForPlayer = false;
    [SerializeField] private searchCone coneOfVision;
    
    [Header ("Chase")]
    [SerializeField] private Transform player;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float catchDistance;

    //Components
    private NavMeshAgent agent;
    private AIAnimation anim;
    float distanceFromTarget;
    
    void Start()
    {
        setPatrolPointsIfNull();
        agent = GetComponent<NavMeshAgent>();
        print(GameObject.FindWithTag("Player").name);
        player = GameObject.FindWithTag("Player").transform;
        
        switch (startingState)
        {
            case (aiState.chase):
                chasePlayer();
                break;
        }
    }
    
    void Update()
    {
        distanceFromTarget = 
            Vector3.Distance(
                gameObject.transform.position, 
                target.position
                );
        
        print(distanceFromTarget);
        
        switch (currentState)
        {
            case (aiState.idle): break;
            case (aiState.waitingBetweenPatrolPoints): break;

            case (aiState.patrol):
                //if (searchForPlayer) { castSearchCone(); }
                
                agent.speed = patrolMoveSpeed;

                target = aiWaypointGroups[waypointGroupIndex]
                    .Waypoints[currentPatrolPointIndex];
                
                if(atTarget())
                
                moveToPatrolPoint(patrolMoveSpeed);
                
                break;

            case (aiState.attack):
                //attack();
                break;

            case (aiState.chase):
                
                target = player;
                
                agent.speed = chaseSpeed;
                agent.destination = target.position;
                
                if(atTarget()) attackPlayer();
                
                break;
        }
    }

    public void chasePlayer() => currentState = aiState.chase;

    public void attackPlayer() => currentState = aiState.attack;
    
    bool atTarget() => distanceFromTarget < catchDistance;

    void setPatrolPointsIfNull()
    {
        if (patrolWaypoints == null)
        {
            try
            {
                patrolWaypoints = aiWaypointGroups[0].Waypoints.ToList();
            }
            catch (UnityException e)
            {
                Debug.Log(e, gameObject);
            }
        }
    }
    
    void checkIfAtTarget()
    {
        if(!target)
        {
            currentState = aiState.patrol;
            setNextPatrolPoint();
        }

        //distance between transform and target transform
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);
    }

    void moveToPatrolPoint(float moveSpeed)
    {
        if (atTarget())
        {
            //stop before proceeding to next target
            StartCoroutine(chooseNextPatrolPoint());
        }
        else
        {
            //move towards target
            agent.SetDestination(target.position);
            agent.speed = moveSpeed;
        }
    }

    void attack()
    {

    }

    void castSearchCone()
    {

    }


    public void setNextPatrolPoint(){ 
        //choose next patrol point based on current path taken
        
    }

    IEnumerator chooseNextPatrolPoint()
    {
    //     //prevent looping
    //     atTarget = true;
        currentState = aiState.waitingBetweenPatrolPoints; 
    
        //anim.setState(AIAnimation.state.look);

        //wait until end of look animation
        yield return new WaitForSecondsRealtime(1);
        
        //anim.setState(AIAnimation.state.walk);
        
        setNextPatrolPoint();

        //enemy no longer at target
        //atTarget = false;
    }
}

[System.Serializable]
class AIWaypointGroup
{
    public Transform[] Waypoints;
}
