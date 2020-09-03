using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStates : MonoBehaviour
{
    NavMeshAgent agent;
    bool atTarget = false;
    [SerializeField]
    Transform[] points;
    [SerializeField]
    Transform target;
    float distanceFromTarget;
    AIAnimation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<AIAnimation>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = points[Random.Range(0, points.Length - 1)];
        anim.setState(AIAnimation.state.walking);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);

        if (distanceFromTarget < 5 && atTarget == false)
        {
            StartCoroutine(endOfTheLine());
        }
        else
        {
            print("distace: " + distanceFromTarget);
            agent.SetDestination(target.position);
        }
    }

    public void setRandomTarget(){ target = points[Random.Range(0, points.Length - 1)]; }

    IEnumerator endOfTheLine()
    {
        atTarget = true;
        anim.setState(AIAnimation.state.looking);
        agent.SetDestination(transform.position);
        yield return new WaitForSecondsRealtime(5.5f);
        anim.setState(AIAnimation.state.walking);
        setRandomTarget();
        atTarget = false;
    }
}
