using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyOutside : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] private static float timeDelay = 5f;
    float delay = timeDelay;
    void Start()
    {
        target = PlayerMovement.Instance.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NavMeshPath path = new NavMeshPath();

        if (agent.CalculatePath(target.position, path))
        {
            agent.destination = target.position;
        }
        else
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                agent.destination = RandomPosition();
                delay = timeDelay;
            }
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100;
        randomDirection += transform.position;
        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, 100, 1);

        return hit.position;
    }
}