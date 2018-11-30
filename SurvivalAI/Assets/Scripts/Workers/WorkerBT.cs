using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Panda;

public class WorkerBT : MonoBehaviour {
    // Behaviour tree
    private PandaBehaviour behaviourTree;

    // Navigation
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private NavMeshPath path;
    private float estimatedTimeUntilArrival;

    // Animation
    private Animator animator;

    // Gathering properties
    public int maxResources = 5;
    public int resourcesCarried;
    private float GatheringTime = 3.0f;
    public ResourceTypes currentlyGathering = ResourceTypes.None;
    private ResourceTypes highestPriorityResource;

    // Tools for gathering
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pickaxe;

    // Resting
    private bool isResting = false;
    [SerializeField] private Transform mainBuilding;
    [SerializeField] private Slider energy;

    // Finders and managers
    private ResourceFinder resourceFinder;
    private StorageFinder storageFinder;
    private ResourceManager resourceManager;
    private WorkersManager workersManager;

    // Hero/Enemy stuff
    private Transform hero;
    public SphereCollider heroDetector;
    public LayerMask enemyLayerMask;


    #region Unity callbacks
    void Start()
    {
        behaviourTree = GetComponent<PandaBehaviour>();

        // Finders and managers
        resourceFinder = GetComponent<ResourceFinder>();
        storageFinder = GetComponent<StorageFinder>();
        resourceManager = FindObjectOfType<ResourceManager>();
        workersManager = FindObjectOfType<WorkersManager>();
        workersManager.workers.Add(this);

        // Navigation
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1, 99);
        agent.enabled = false;
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = true;

        energy.value = 100;

        animator = GetComponent<Animator>();

        // Hero stuff
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        heroDetector = GetComponentInChildren<SphereCollider>();
    }

    private void OnDrawGizmos()
    {
        if (agent != null && agent.destination != Vector3.zero)
            Gizmos.DrawWireCube(agent.destination, Vector3.one);
    }
    #endregion

    #region State machine checks
    [Task]
    bool IsThreatened()
    {
        return animator.GetBool("IsThreatened");
    }

    [Task]
    bool IsWalking()
    {
        return animator.GetBool("IsWalking");
    }
    
    [Task]
    bool IsWalkingToResource()
    {
        if (!animator.GetBool("IsFleeing"))
        {
            if (animator.GetBool("IsGathering") && animator.GetBool("IsWalking") && !animator.GetBool("IsFull"))
            {
                return true;
            }
        }
        return false;
    }
    
    [Task]
    bool IsGathering()
    {
        return animator.GetBool("IsGathering");
    }

    [Task]
    bool IsFull()
    {
        return animator.GetBool("IsFull");
    }
    
    [Task]
    bool IsTired()
    {
        return animator.GetBool("IsTired");
    }
    #endregion

    #region Enemy reactions
    [Task]
    void RunToHero()
    {
        obstacle.enabled = false;
        agent.enabled = true;
        agent.destination = hero.position;

        Task.current.Succeed();
    }
    #endregion

    #region Resting actions
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

    // Get some energy back when resting at main building
    [Task]
    void RestAtMainBuilding()
    {
        isResting = true;
        transform.localScale = Vector3.zero;
        obstacle.enabled = false;
        energy.value += 1f;
        animator.SetFloat("Energy", energy.value);

        Task.current.Succeed();
    }
    #endregion

    #region Gathering actions
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
            if (new_resourcePriority > resourcePriority)
            {
                highestPriorityResource = resource.Key;
                resourcePriority = new_resourcePriority;
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
        Transform resourceToGather = resourceFinder.FindClosest(currentlyGathering);
        obstacle.enabled = false;
        agent.enabled = true;
        agent.destination = resourceToGather.position;
        Task.current.Succeed();
    }

    [Task]
    void GatherResource()
    {
        animator.SetBool("IsGathering", true);
        agent.enabled = false;
        obstacle.enabled = true;
        energy.value -= 1;
        animator.SetFloat("Energy", energy.value);
        resourcesCarried += 1;

        Task.current.Succeed();
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
    #endregion
}
