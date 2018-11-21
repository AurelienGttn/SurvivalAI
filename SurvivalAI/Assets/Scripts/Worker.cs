using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour {

    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    [SerializeField] private int maxResources = 5;
    private int resourcesCarried;
    private float GatheringTime = 3.0f;
    
    private ResourceFinder resourceFinder;
    private StorageFinder storageFinder;
    private ResourceManager resourceManager;

    public enum State
    {
        Idle,
        Gathering,
        WalkingToResource,
        WalkingToWarehouse
    }

    public State state;

    private enum Resource
    {
        Wood,
        Stone,
        Food
    }

    private Resource? currentlyGathering = null;
    public string currentResource;

    void Start () {
        state = State.Idle;

        resourceFinder = GetComponent<ResourceFinder>();
        storageFinder = GetComponent<StorageFinder>();
        resourceManager = FindObjectOfType<ResourceManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1, 99);
        agent.enabled = false;
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = true;
	}
	
	void Update () {
        // Cancel current action
        if (Input.GetKeyDown("x"))
        {
            state = State.Idle;
            currentlyGathering = null;
            agent.destination = transform.position;
        }

        // Select resource to gather
        if (state == State.Idle)
        {
            if (Input.GetKeyDown("w"))
            {
                currentlyGathering = Resource.Wood;
            }
            else if (Input.GetKeyDown("s"))
            {
                currentlyGathering = Resource.Stone;
            }
            else if (Input.GetKeyDown("f"))
            {
                currentlyGathering = Resource.Food;
            }

            if (currentlyGathering != null)
            {
                currentResource = currentlyGathering.ToString();
                WalkToResource(currentResource);
            }
        }

        // if arrived at resource, start gathering
        if (state == State.WalkingToResource)
        {
            if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
            {
                transform.LookAt(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
                StartCoroutine(GatherResource());
            }
            else
            {
                WalkToResource(currentResource);
            }
        }

        // if arrived at warehouse, deposit resource
        if (state == State.WalkingToWarehouse)
        {
            if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
            {
                transform.LookAt(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
                StartCoroutine(DepositResource());
            }
            else
            {
                WalkToWarehouse();
            }
        }

    }


    private void WalkToResource(string resourceType)
    {
        obstacle.enabled = false;
        Transform resourceToGather = resourceFinder.FindClosest(resourceType);
        agent.enabled = true;
        agent.destination = resourceToGather.position;
        state = State.WalkingToResource;
    }

    private IEnumerator GatherResource()
    {
        agent.enabled = false;
        state = State.Gathering;
        obstacle.enabled = true;
        yield return new WaitForSeconds(GatheringTime);
        resourcesCarried += maxResources;
        Debug.Log("Harvested " + resourcesCarried + " " + currentResource);
        WalkToWarehouse();
    }

    private void WalkToWarehouse()
    {
        obstacle.enabled = false;
        Transform warehouse = storageFinder.FindClosest();
        agent.enabled = true;
        Vector3 warehouseClosestBound = (transform.position - warehouse.position).normalized;
        warehouseClosestBound = warehouseClosestBound * warehouse.localScale.x / 2;
        agent.destination = warehouseClosestBound;
        state = State.WalkingToWarehouse;
    }

    private IEnumerator DepositResource()
    {
        agent.enabled = false;
        yield return new WaitForSeconds(1.0f);
        obstacle.enabled = true;
        state = State.Idle;
        resourceManager.AddResource(currentResource, resourcesCarried);
        Debug.Log("Deposit " + resourcesCarried + " " + currentResource);
        resourcesCarried = 0;
        WalkToResource(currentResource);
    }
}
