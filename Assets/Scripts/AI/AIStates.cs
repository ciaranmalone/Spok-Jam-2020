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

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = points[Random.Range(0, points.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.position);

        if(distanceFromTarget < 5)
        {
            setRandomTarget();
        }
        else
        {
            print("distace: " + distanceFromTarget);
            agent.SetDestination(target.position);
        }
    }

    public void setRandomTarget(){ target = points[Random.Range(0, points.Length - 1)]; }
}
