using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Panda;

public class WorkerBT : MonoBehaviour {

    // Reproduction
    [SerializeField] private GameObject workerPrefab;
    private Transform workersParent;

    // Navigation
    private NavMeshAgent agent;

    // Animation
    private Animator animator;

    // Gathering parameters
    public int maxResources = 5;
    public int resourcesCarried;
    private float exactResourcesCarried = 0;
    public ResourceTypes currentlyGathering = ResourceTypes.None;
    private ResourceTypes highestPriorityResource;
    private Transform resourceToGather;
    // Tools for gathering
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pickaxe;

    // Resting
    private Transform mainBuilding;
    [SerializeField] private Slider energy;

    // Finders and managers
    private ResourceFinder resourceFinder;
    private StorageFinder storageFinder;
    private ResourceManager resourceManager;
    private WorkersManager workersManager;
    private ConstructionManager constructionManager;

    // Hero/Enemy stuff
    private Transform hero;
    public SphereCollider heroDetector;
    public LayerMask enemyLayerMask;


    #region Unity callbacks
    void Start()
    {
        // Reproduction
        workersParent = GameObject.Find("Workers").transform;

        // Finders and managers
        resourceFinder = GetComponent<ResourceFinder>();
        storageFinder = GetComponent<StorageFinder>();
        resourceManager = FindObjectOfType<ResourceManager>();
        workersManager = FindObjectOfType<WorkersManager>();
        workersManager.workers.Add(this);
        constructionManager = FindObjectOfType<ConstructionManager>();

        // Navigation
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1, 99);
        agent.enabled = true;

        // Resting
        mainBuilding = GameObject.Find("MainBuilding").transform;
        energy.value = 100;

        animator = GetComponent<Animator>();

        // Hero stuff
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        heroDetector = GetComponentInChildren<SphereCollider>();
    }

    private void Update()
    {
        if (agent.hasPath)
        {
            transform.LookAt(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
        }
    }

    private void OnDrawGizmos()
    {
        if (agent != null && agent.destination != Vector3.zero)
            Gizmos.DrawWireCube(agent.destination, Vector3.one);
    }
    #endregion

    #region Utility functions

    // Destination is not a point it's an object, and we actually want 
    // to go to the closest point of this object. We calculate this point
    // with the normalized distance and the scale of the object.
    private Vector3 GetClosestBound(Transform target)
    {
        Vector3 targetClosestBound = (transform.position - target.position).normalized;
        targetClosestBound = targetClosestBound * target.localScale.x / 2 + target.position;

        return targetClosestBound;
    }

    // Create a new worker as a result of mating
    private void CreateNewWorker()
    {
        Instantiate(workerPrefab, new Vector3(3,1,3), Quaternion.identity, workersParent);
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
    bool CanBuild()
    {
        return animator.GetBool("CanBuild");
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
        if (hero != null)
        {
            agent.destination = hero.position;
        }

        Task.current.Succeed();
    }
    #endregion

    #region Resting actions
    [Task]
    void WalkToMainBuilding()
    {
        agent.destination = GetClosestBound(mainBuilding);
        animator.SetBool("IsWalking", Vector3.Distance(agent.destination, transform.position) > agent.stoppingDistance + 0.5);

        Task.current.Succeed();
    }

    // Get some energy back when resting at main building
    [Task]
    void RestAtMainBuilding()
    {
        transform.localScale = Vector3.zero;
        energy.value += 1f;
        animator.SetFloat("Energy", energy.value);

        Task.current.Succeed();
    }

    [Task]
    void TryToMate()
    {
        if (Random.Range(0, 1) < 1 / (workersManager.workers.Count + 1))
        {
            CreateNewWorker();
        }

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
            int currentWorkers = workersManager.gatheringWorkers[resource.Key];
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
        workersManager.gatheringWorkers[currentlyGathering]++;

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
        resourceToGather = resourceFinder.FindClosest(currentlyGathering);
        agent.destination = GetClosestBound(resourceToGather);
        animator.SetBool("IsWalking", Vector3.Distance(agent.destination, transform.position) > agent.stoppingDistance + 0.5);

        Task.current.Succeed();
    }

    [Task]
    void GatherResource()
    {
        if (resourceToGather.GetComponent<HealthManager>().currentHealth <= 0)
        {
            animator.SetBool("IsGathering", false);
            Task.current.Fail();
        }

        animator.SetBool("IsGathering", true);
        energy.value -= 0.02f;
        animator.SetFloat("Energy", energy.value);
        // Set it low enough to have several animations
        exactResourcesCarried += 0.05f;
        resourcesCarried = (int)Mathf.Floor(exactResourcesCarried);
        resourceToGather.GetComponent<HealthManager>().TakeDamage(0.05f);

        Task.current.Succeed();
    }

    [Task]
    void WalkToClosestWarehouse()
    {
        animator.SetBool("IsGathering", false);
        Transform warehouse = storageFinder.FindClosest();
        agent.destination = GetClosestBound(warehouse);
        animator.SetBool("IsWalking", Vector3.Distance(agent.destination, transform.position) > agent.stoppingDistance + 0.5);

        Task.current.Succeed();
    }

    [Task]
    void DepositResources()
    {
        resourceManager.AddResource(currentlyGathering, resourcesCarried);
        workersManager.gatheringWorkers[currentlyGathering]--;
        currentlyGathering = ResourceTypes.None;
        resourcesCarried = 0;
        exactResourcesCarried = 0;

        Task.current.Succeed();
    }
    #endregion


    #region Building actions

    [Task]
    void CheckBuildingsReady()
    {
        Task.current.Complete(constructionManager.constructionList.Count > 0);
    }

    [Task]
    void WalkToReadyBuilding()
    {
        Transform buildingToBuild = constructionManager.constructionList[0].transform;
        agent.destination = GetClosestBound(buildingToBuild);
        workersManager.buildingWorkers++;
        animator.SetBool("IsWalking", Vector3.Distance(agent.destination, transform.position) > agent.stoppingDistance + 0.5);

        Task.current.Succeed();
    }

    [Task]
    void Build()
    {
        Transform currentlyBuilding = constructionManager.constructionList[0].transform;
        animator.SetBool("IsBuilding", true);
        energy.value -= 0.02f;
        animator.SetFloat("Energy", energy.value);
        // Set it low enough to have several animations
        currentlyBuilding.GetComponent<HealthManager>().Heal(0.05f);
        if (currentlyBuilding.GetComponent<HealthManager>().currentHealth >= currentlyBuilding.GetComponent<HealthManager>().maxHealth)
        {
            animator.SetBool("IsBuilding", false);
            workersManager.buildingWorkers--;
        }

        Task.current.Succeed();
    }
    #endregion
    
}
