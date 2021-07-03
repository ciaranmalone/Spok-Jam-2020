using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIStates : MonoBehaviour
{
    private Transform target;

    [Header("States")]
    [SerializeField] private aiState startingState;
    [SerializeField] private aiState currentState;
    private enum aiState { patrol, chase, attack, idle, waitingBetweenPatrolPoints };

    [Header("Patrol")]
    [SerializeField] private float patrolMoveSpeed;
    [SerializeField] private float patrolStoppingDistance;
    [SerializeField] private float patrolWaypointWaitTime = 2f;
    private WaitForSecondsRealtime patrolWait;
    [SerializeField] private int waypointGroupIndex = 0;
    [SerializeField] private AIWaypointGroup[] aiWaypointGroups;
    private List<Transform> patrolWaypoints;
    private int currentPatrolPointIndex;

    [Header("Search")]

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool searchForPlayer = false;
    [SerializeField] private float coneAngle = 30f;

    [Header("Chase")]
    private Transform player;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float catchDistance;

    //Components
    private NavMeshAgent agent;
    private AIAnimation anim;
    NavMeshPath path;

    void Start()
    {
        patrolWait = new WaitForSecondsRealtime(patrolWaypointWaitTime); //Create instance of WaitForSecondsRealtime for use between patrol points
        path = new NavMeshPath();

        setPatrolPointsIfNull();
        agent = GetComponent<NavMeshAgent>();
        //        print(GameObject.FindWithTag("Player").name);
        player = PlayerMovement.Instance.transform;

        target = player; //ensure target is never null

        switch (startingState)
        {
            case (aiState.chase):
                chasePlayer();
                break;
        }
    }

    void Update()
    {
        castSearchCone();

        switch (currentState)
        {
            case (aiState.idle): break;

            case (aiState.waitingBetweenPatrolPoints): break;

            case (aiState.patrol):
                //if (searchForPlayer) { castSearchCone(); }

                agent.speed = patrolMoveSpeed;

                if (atTarget(patrolStoppingDistance))
                {
                    if (currentPatrolPointIndex ==
                        aiWaypointGroups[waypointGroupIndex]
                            .Waypoints.Length - 1)
                    {
                        currentPatrolPointIndex = 0;
                    }
                    else
                    {
                        currentPatrolPointIndex++;
                    }

                    StartCoroutine(waitAtPatrolPoint());
                    break;
                }

                target = aiWaypointGroups[waypointGroupIndex]
                    .Waypoints[currentPatrolPointIndex];


                agent.SetDestination(target.position);
                agent.speed = patrolMoveSpeed;

                break;


            case (aiState.attack):
                //attack();
                break;


            case (aiState.chase):

                target = player;

                agent.speed = chaseSpeed;
                agent.destination = target.position;

                if (atTarget(catchDistance)) attackPlayer();

                if (!agent.CalculatePath(target.position, path)) currentState = aiState.patrol;

                break;
        }
    }

    public void chasePlayer() => currentState = aiState.chase;

    public void attackPlayer() => currentState = aiState.attack;

    float distanceFromTarget() => Vector3.Distance(
        gameObject.transform.position,
        target.position
        );

    bool atTarget(float stoppingDistance) => distanceFromTarget() < stoppingDistance;

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
        if (!target)
        {
            currentState = aiState.patrol;
            setNextPatrolPoint();
        }

        //distance between transform and target transform
        //distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);
    }

    IEnumerator waitAtPatrolPoint()
    {
        currentState = aiState.waitingBetweenPatrolPoints;

        yield return patrolWait;

        target = aiWaypointGroups[waypointGroupIndex]
            .Waypoints[currentPatrolPointIndex];

        currentState = aiState.patrol;
    }

    void attack()
    {

    }

    void castSearchCone()
    {
        Vector3 shannonsEyes = transform.position + new Vector3(0, 2, 0);
        RaycastHit hit;
        
        Debug.DrawLine(transform.position, player.position, Color.red);
    
        if (Physics.Linecast(shannonsEyes, player.position, out hit))
        {
            float angle = Vector3.Angle(transform.forward*-1, hit.normal);
         
            if(hit.transform == player && angle < coneAngle){

                currentState = aiState.chase;
            }
        } 
    }


    public void setNextPatrolPoint()
    {
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
