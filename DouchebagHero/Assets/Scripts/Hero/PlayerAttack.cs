using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damage;
    public Collider hitBox;
    private Animator animator;

    private bool canAttack;
    
	void Start ()
    {
        animator = GetComponent<Animator>();
        canAttack = true;
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Attack();
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        animator.SetTrigger("AttackTrigger");
        Collider[] colliders = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents);
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                HealthManager enemyHealth = col.GetComponent<HealthManager>();
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}
