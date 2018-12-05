using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent.enabled = true;
        obstacle.enabled = false;
	}

    public void MoveTo(Vector3 destination)
    {
        if (Vector3.Distance(destination, transform.position) > agent.stoppingDistance)
        {
            StartCoroutine(MoveToTarget(destination));
        }

    }

    private IEnumerator MoveToTarget(Vector3 destination)
    {
        obstacle.enabled = false;
        //yield return null;
        agent.enabled = true;
        agent.destination = destination;
        while (agent.hasPath)
        {
            yield return null;
        }

        //agent.enabled = false;
        //obstacle.enabled = true;
    }



}
