using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private WorkerBT workerBT;
    private WorkersManager workersManager;
    private ConstructionManager constructionManager;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        workerBT = GetComponent<WorkerBT>();
        workersManager = FindObjectOfType<WorkersManager>();
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    private void Update()
    {
        ///////////// CHECK CURRENT ACTION /////////////

        // Check if worker is trying to gather something
        animator.SetBool("IsGathering", workerBT.currentlyGathering != ResourceTypes.None);

        // Check if worker has reached its target
        animator.SetBool("IsWalking", Vector3.Distance(agent.destination, transform.position) >= agent.stoppingDistance + 0.5);

        // Check if worker's inventory is full
        animator.SetBool("IsFull", workerBT.resourcesCarried >= workerBT.maxResources);

        // Check if enemy is nearby
        Collider[] enemyCol = Physics.OverlapSphere(transform.position, workerBT.heroDetector.radius, workerBT.enemyLayerMask);
        animator.SetBool("IsThreatened", enemyCol.Length > 0);

        // Check if worker is tired
        animator.SetBool("IsResting", animator.GetFloat("Energy") < 95);
        if (animator.GetFloat("Energy") < 20)
        {
            animator.SetBool("IsTired", true);
        }
        if (animator.GetFloat("Energy") >= 20 && !animator.GetBool("IsResting"))
        {
            animator.SetBool("IsTired", false);
        }

        // Check if building is ready to be built
        animator.SetBool("CanBuild", constructionManager.waitingList.Count > 0 && workersManager.buildingWorkers < 3 || workerBT.currentlyBuilding != null);
        if (!animator.GetBool("CanBuild"))
        {
            animator.SetBool("IsBuilding", false);
        }
        
    }
}