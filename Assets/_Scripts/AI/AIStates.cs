using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStates : MonoBehaviour {
    [Header ("States")]
    public aiState currentState;
    public enum aiState {patrol, search, chase, attack};
    

    [Header ("Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AIAnimation anim;
    float distanceFromTarget;


    [Header ("Patrol")]
    [SerializeField] float patrolMoveSpeed;
    [SerializeField] AIWaypointGroup[] aiWaypointGroups;
    Transform patrolTarget;
    string patrolPointTargetGroup = "back";
    bool atTarget = false;

    [Header ("Search")]
    [SerializeField] bool searchForPlayer = false;
    [SerializeField] searchCone coneOfVision;
    Transform searchTarget;
    bool searchingForPlayer = false;

    [Header ("Chase")]
    [SerializeField] Transform player;
    [SerializeField] float chaseSpeed;

    

    void Start()
    {
        /*anim = gameObject.GetComponent<AIAnimation>();
        agent = gameObject.GetComponent<NavMeshAgent>();*/
        
        anim.setState(AIAnimation.state.run);
    }

    void Update()
    {
        switch (currentState)
        {
            case (aiState.patrol):
                if (searchForPlayer) { castSearchCone(); }
                checkIfAtTarget(patrolTarget);
                moveToTarget(patrolMoveSpeed, patrolTarget);
                break;

            case (aiState.attack):
                attack();
                break;

            case (aiState.search):
                if (searchForPlayer) { castSearchCone(); }
                checkIfAtTarget(searchTarget);
                moveToTarget(patrolMoveSpeed, patrolTarget);
                break;
        }
    }

    void checkIfAtTarget(Transform target)
    {
        if(target == null)
        {
            currentState = aiState.patrol;
            setNextPatrolPoint();
        }

        //distance between transform and target transform
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);
    }

    void moveToTarget(float moveSpeed, Transform moveTarget)
    {
        //atTarget bool prevents looping once at target.
        if (distanceFromTarget < 0.5 && atTarget == false)
        {
            //stop before proceeding to next target
            StartCoroutine(chooseNextPatrolPoint());
        }
        else
        {
            //move towards target
            agent.SetDestination(moveTarget.position);
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
        //prevent looping
        atTarget = true;

        anim.setState(AIAnimation.state.look);

        //wait until end of look animation
        yield return new WaitForSecondsRealtime(5.5f);
        
        anim.setState(AIAnimation.state.walk);
        
        setNextPatrolPoint();

        //enemy no longer at target
        atTarget = false;
    }
}

[System.Serializable]
class AIWaypointGroup
{
    [SerializeField] private Transform[] Waypoints;
}
