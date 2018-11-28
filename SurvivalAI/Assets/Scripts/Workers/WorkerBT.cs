﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Panda;

public class WorkerBT : MonoBehaviour {
    private PandaBehaviour behaviourTree;
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private Animator animator;
    [SerializeField] private int maxResources = 5;
    private int resourcesCarried;
    private float GatheringTime = 3.0f;
    private bool isResting = false;
    [SerializeField] private Transform mainBuilding;

    private ResourceFinder resourceFinder;
    private StorageFinder storageFinder;
    private ResourceManager resourceManager;
    private WorkersManager workersManager;

    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pickaxe;
    [SerializeField] private Slider energy;

    public ResourceTypes currentlyGathering = ResourceTypes.None;
    public string currentResource;
    private ResourceTypes highestPriorityResource;

    private NavMeshPath path;
    private float estimatedTimeUntilArrival;

    void Start()
    {
        behaviourTree = GetComponent<PandaBehaviour>();
        resourceFinder = GetComponent<ResourceFinder>();
        storageFinder = GetComponent<StorageFinder>();
        resourceManager = FindObjectOfType<ResourceManager>();
        workersManager = FindObjectOfType<WorkersManager>();
        workersManager.workers.Add(this);

        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1, 99);
        agent.enabled = false;
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = true;
        energy.value = 40;

        animator = GetComponent<Animator>();
    }

    [Task]
    bool IsRested()
    {
        if (energy.value > 95)
        {
            isResting = false;
        }
        return true;
    }

    [Task]
    bool IsTired()
    {
        if (!isResting)
            return energy.value < 20;
        return true;
    }

    [Task]
    void WalkToMainBuilding()
    {
        obstacle.enabled = false;
        agent.enabled = true;
        Vector3 closestBound = (transform.position - mainBuilding.position).normalized;
        closestBound = closestBound * mainBuilding.localScale.x / 2;
        agent.destination = closestBound;

        Task.current.Succeed();
    }

    // Check if the agent arrived at its destination
    [Task]
    void IsArrivedAtDestination()
    {
        path = new NavMeshPath();
        if (agent.CalculatePath(agent.destination, path) && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            estimatedTimeUntilArrival = agent.remainingDistance / agent.speed;
            behaviourTree.Wait(estimatedTimeUntilArrival + 1f);
        }
    }

    // Get some energy back when resting at main building
    [Task]
    void RestAtMainBuilding()
    {
        isResting = true;
        transform.localScale = Vector3.zero;
        obstacle.enabled = false;
        energy.value += 5f;
        Debug.Log("rested");

        Task.current.Succeed();
    }

    // Check if any resource is needed
    [Task]
    bool HasEnoughResources()
    {
        foreach (KeyValuePair<ResourceTypes, int> resource in resourceManager.resourcesNeeded)
        {
            if (resource.Value > 0)
                return false;
        }
        return true;
    }

    // Choose the resource to collect based on how much is needed
    // and how many workers are already collecting this resource
    [Task]
    void ChooseResource()
    {
        transform.localScale = Vector3.one;
        float resourcePriority = 0;
        foreach (KeyValuePair<ResourceTypes, int> resource in resourceManager.resourcesNeeded)
        {
            int currentWorkers = workersManager.workersOccupation[resource.Key];
            // Add 1 to avoid dividing by 0
            float new_resourcePriority = resource.Value / (currentWorkers + 1);
            Debug.Log(resource.Key.ToString() + "resource prio = " + new_resourcePriority);
            if (new_resourcePriority > resourcePriority)
            {
                highestPriorityResource = resource.Key;
                resourcePriority = new_resourcePriority;
                Debug.Log(highestPriorityResource.ToString());
            }
        }
        currentlyGathering = highestPriorityResource;
        // Add this worker to the worker manager, to update priorities
        workersManager.workersOccupation[currentlyGathering]++;

        Task.current.Complete(currentlyGathering != ResourceTypes.None);
    }

    [Task]
    void ChooseTool()
    {
        if (currentlyGathering == ResourceTypes.Wood)
        {
            pickaxe.SetActive(false);
            axe.SetActive(true);
        }
        else if (currentlyGathering == ResourceTypes.Stone)
        {
            axe.SetActive(false);
            pickaxe.SetActive(true);
        }
        else
        {
            axe.SetActive(false);
            pickaxe.SetActive(false);
        }

        Task.current.Succeed();
    }

    [Task]
    void WalkToClosestResource()
    {
        obstacle.enabled = false;
        Transform resourceToGather = resourceFinder.FindClosest(currentlyGathering);
        agent.enabled = true;
        agent.destination = resourceToGather.position;

        Task.current.Complete(agent.pathStatus == NavMeshPathStatus.PathComplete);
    }

    [Task]
    void GatherResource()
    {
        animator.SetBool("IsGathering", true);
        agent.enabled = false;
        obstacle.enabled = true;
        energy.value -= 5;
        resourcesCarried += maxResources;

        Task.current.Succeed();
    }

    [Task]
    void WaitForGatheringEnd()
    {
        behaviourTree.Wait(GatheringTime);
    }

    [Task]
    void WalkToClosestWarehouse()
    {
        animator.SetBool("IsGathering", false);
        obstacle.enabled = false;
        Transform warehouse = storageFinder.FindClosest();
        agent.enabled = true;
        Vector3 warehouseClosestBound = (transform.position - warehouse.position).normalized;
        warehouseClosestBound = warehouseClosestBound * warehouse.localScale.x / 2;
        agent.destination = warehouseClosestBound;

        Task.current.Succeed();
    }

    [Task]
    void DepositResources()
    {
        agent.enabled = false;
        obstacle.enabled = true;
        resourceManager.AddResource(currentlyGathering, resourcesCarried);
        workersManager.workersOccupation[currentlyGathering]--;
        currentlyGathering = ResourceTypes.None;
        resourcesCarried = 0;

        Task.current.Succeed();
    }

    private void OnDrawGizmos()
    {
        if (agent != null && agent.destination != Vector3.zero)
            Gizmos.DrawWireCube(agent.destination, Vector3.one);
    }
}