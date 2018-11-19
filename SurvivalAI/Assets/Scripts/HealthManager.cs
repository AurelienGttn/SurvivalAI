using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int health = 100;

    private int currentHealth;

	// Use this for initialization
	void Start ()
        {
        currentHealth = health;
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("I'm dead");
        }
    }
}
