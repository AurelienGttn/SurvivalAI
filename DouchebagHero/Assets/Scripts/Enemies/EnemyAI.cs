using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private Animator animator;
    private Transform mainBuilding;

    public int damage;
    public Collider hitBox;
    public SphereCollider aggroZone;
    public LayerMask aggroLayerMask;
    private HealthManager enemyHealth;

    public bool isAttacking;
    private bool canAttack;
    [SerializeField] private float attackCooldown = 1;
    public Transform target;
    
    void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        agent.avoidancePriority = Random.Range(1, 99);

        if (GameObject.FindGameObjectWithTag("MainBuilding"))
            mainBuilding = GameObject.FindGameObjectWithTag("MainBuilding").transform;
        target = mainBuilding;
        agent.destination = GetClosestBound();

        canAttack = true;
        isAttacking = false;
    }

    void FixedUpdate()
    {
        // If there's already a target, check if it's still in range
        if (animator.GetBool("Locked"))
        {
            Transform target_temp = CheckAttackRange();
            if (target_temp == null || target_temp.name != target.name)
                animator.SetBool("Locked", false);
            else {
                // and attack it if cooldown is up
                if (canAttack)
                {
                    Attack();
                    CheckEnemyLife();
                }
            }
        }

        // Look for something to attack
        else 
        {
            // First check if an enemy is in the attack range
            target = CheckAttackRange();
            if (target != null)
            {
                animator.SetBool("Locked", true);
                enemyHealth = target.GetComponent<HealthManager>();
                // If the attack is not on cooldown, attack enemy then check if he's dead
                if (canAttack)
                {
                    Attack();
                    CheckEnemyLife();
                }
            }

            // If there is no enemy in attack range, check aggro zone
            else
            {
                target = CheckAggroZone();
                // Make sure the target isn't out of aggro zone
                if (target != null)
                {
                    animator.SetBool("Locked", true);
                    CheckDistanceFromTarget();
                }
            }
        }
    }

    private void Attack() {
        animator.SetBool("isAttacking", true);
        agent.enabled = false;
        obstacle.enabled = true;
        canAttack = false;

        if (enemyHealth != null)
            enemyHealth.TakeDamage(damage);

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttacking", false);
        canAttack = true;
    }

    // If enemy is dead, reinitialize target
    private void CheckEnemyLife()
    {
        if (enemyHealth != null)
        {
            if (enemyHealth.currentHealth <= 0)
            {
                animator.SetBool("isAttacking", false);
                target = mainBuilding;
                GetClosestBound();
            }
        }
    }

    private Transform CheckAttackRange()
    {
        Collider[] withinAttackRange = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents, Quaternion.identity, aggroLayerMask);
        if (withinAttackRange.Length > 0)
        {
            return withinAttackRange[0].transform;
        }
        return null;
    }

    private Transform CheckAggroZone()
    {
        Collider[] withinAggroZone = Physics.OverlapSphere(transform.position, aggroZone.radius, aggroLayerMask);
        if (withinAggroZone.Length > 0)
        {
            return withinAggroZone[0].transform;
        }

        return mainBuilding;
    }
    
    private void CheckDistanceFromTarget()
    {
        if (Vector3.Distance(target.position, agent.transform.position) > aggroZone.radius)
        {
            target = mainBuilding;
            agent.destination = GetClosestBound();
        }
        else
        {
            agent.destination = GetClosestBound();
            transform.LookAt(target);
        }
    }

    // Returns the closest wall of the main building
    private Vector3 GetClosestBound()
    {
        obstacle.enabled = false;
        agent.enabled = true;
        Vector3 destinationClosestBound = Vector3.zero;
        if (target != null)
        {
            destinationClosestBound = target.position + (transform.position - target.position).normalized * (target.localScale.x / 2);
        }
        
        return destinationClosestBound;
    }

    private void OnDrawGizmos()
    {
        if (agent != null && agent.destination != Vector3.zero)
            Gizmos.DrawWireSphere(agent.destination, 1f);
    }
}
