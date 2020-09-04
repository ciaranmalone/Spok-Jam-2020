using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStates : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    bool atTarget = false;
    [SerializeField]
    Transform[] frontPatrolPoints;
    [SerializeField]
    Transform[] centrePatrolPoints;
    [SerializeField]
    Transform[] backPatrolPoints;
    [SerializeField]
    Transform target;
    [SerializeField]
    string targetGroup;
    float distanceFromTarget;
    AIAnimation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<AIAnimation>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = frontPatrolPoints[Random.Range(0, frontPatrolPoints.Length - 1)];
        targetGroup = target.transform.parent.name;
        anim.setState(AIAnimation.state.walking);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);

        if (distanceFromTarget < 0.5 && atTarget == false)
        {
            StartCoroutine(endOfTheLine());
        }
        else
        {
            //print("distace: " + distanceFromTarget);
            agent.SetDestination(target.position);
        }
    }

    public void setRandomTarget(){ target = centrePatrolPoints[Random.Range(0, centrePatrolPoints.Length - 1)]; }

    IEnumerator endOfTheLine()
    {
        print("points: " + centrePatrolPoints.Length);
        atTarget = true;
        anim.setState(AIAnimation.state.looking);
        agent.SetDestination(transform.position);
        print("waiting to walk");
        yield return new WaitForSecondsRealtime(5.5f);
        print("resume walking");
        anim.setState(AIAnimation.state.walking);
        setRandomTarget();
        atTarget = false;
    }
}
