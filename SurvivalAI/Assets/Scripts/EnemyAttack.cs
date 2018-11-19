using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int damage;
    public Collider hitBox;
    private Animator animator;

    private bool canAttack;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        canAttack = true;
    }
	
	// Update is called once per frame
	void Update () {
        Collider[] colliders = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents);
        foreach (Collider col in colliders)
        {
            if ((col.tag == "Player" || col.tag == "Worker") && canAttack)
            {
                canAttack = false;
                animator.Play("Attack");
                Debug.Log("enemy hit");
                HealthManager enemyHealth = col.GetComponent<HealthManager>();
                enemyHealth.TakeDamage(damage);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}
