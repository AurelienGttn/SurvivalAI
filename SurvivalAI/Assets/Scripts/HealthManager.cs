using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    [SerializeField] private ParticleSystem deathAnimation;
    public int health = 100;
    public int currentHealth;
    
	void Start ()
        {
        currentHealth = health;
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            ParticleSystem deathAnim = Instantiate(deathAnimation, gameObject.transform);
            Destroy(gameObject, 1f);
        }
    }
}
