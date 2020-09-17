using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStates : MonoBehaviour {
    enum aiState {patrol, search, chase, attack};
    aiState currentState;
    NavMeshAgent agent;
    Transform target;

    //patrol variables
    [SerializeField] float patrolMoveSpeed;
    [SerializeField] string patrolPointTargetGroup = "back";
    [SerializeField] Transform[] frontPatrolPoints;
    [SerializeField] Transform[] centrePatrolPoints;
    [SerializeField] Transform[] backPatrolPoints;
    [SerializeField] bool searchForPlayer = false;
    bool atTarget = false;
    
    float distanceFromTarget;
    AIAnimation anim;

    void Start()
    {
        //assign components
        anim = gameObject.GetComponent<AIAnimation>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        
        target = frontPatrolPoints[Random.Range(0, frontPatrolPoints.Length - 1)];
        anim.setState(AIAnimation.state.run);
    }

    void Update()
    {
        switch (currentState)
        {
            case (aiState.patrol):
                checkIfAtTarget();
                moveToTarget();
                break;

            case (aiState.attack):

                break;

            case (aiState.search):

                break;
        }
    }

    void checkIfAtTarget()
    {
        //distance between transform and target transform
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);
    }

    void moveToTarget()
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
            agent.SetDestination(target.position);
        }
    }

    void castSearchCone()
    {

    }


    public void setNextPatrolPoint(){ 
        //choose next patrol point based on current path taken
        switch (patrolPointTargetGroup)
        {
            case ("back"):
                target = centrePatrolPoints[Random.Range(0, centrePatrolPoints.Length - 1)];
                patrolPointTargetGroup = "back -> centre";
                break;

            case ("back -> centre"):
                target = frontPatrolPoints[Random.Range(0, frontPatrolPoints.Length - 1)];
                break;

            case ("front"):
                target = centrePatrolPoints[Random.Range(0, centrePatrolPoints.Length - 1)];
                patrolPointTargetGroup = "front -> centre";
                break;

            case ("front -> centre"):
                target = backPatrolPoints[Random.Range(0, backPatrolPoints.Length - 1)];
                break;
        }
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
