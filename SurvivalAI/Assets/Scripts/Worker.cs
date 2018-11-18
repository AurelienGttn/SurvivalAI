using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour {

    private NavMeshAgent agent;
    [SerializeField] private int maxResources = 5;
    private float GatheringTime = 3.0f;
    private bool isIdle, isGathering, isWalkingToResource, isWalkingToStorage;

    private Transform resourceToGather;
    private ResourceFinder resourceFinder;

	void Start () {
        isIdle = true;
        isGathering = false;
        isWalkingToResource = false;
        isWalkingToStorage = false;

        resourceFinder = GetComponentInChildren<ResourceFinder>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        if (!isGathering)
        {
            if (Input.GetKeyDown("w"))
            {
                WalkToResource("Wood");
            }
            else if (Input.GetKeyDown("r"))
            {
                WalkToResource("Rock");
            }
            else if (Input.GetKeyDown("b"))
            {
                WalkToResource("Berry");
            }
        }
        if (isWalkingToResource == true && Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            isWalkingToResource = false;
            isGathering = true;
            StartCoroutine(GatherResource());
        }
	}


    private void WalkToResource(string resourceType)
    {
        isWalkingToResource = true;
        resourceToGather = resourceFinder.FindClosest(resourceType);
        agent.destination = resourceToGather.position;
    }

    private IEnumerator GatherResource()
    {
        yield return new WaitForSeconds(GatheringTime);
        Debug.Log("Got " + maxResources +" resources");
        isGathering = false;
    }
}
