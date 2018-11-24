using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [SerializeField] private ParticleSystem deathAnimation;
    public int health = 100;
    public int currentHealth;
    [SerializeField] private Slider healthBar;
    
	void Start ()
    {
        currentHealth = health;
        healthBar.maxValue = health;
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            if (gameObject.GetComponent<Renderer>().enabled)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                ParticleSystem deathAnim = Instantiate(deathAnimation, gameObject.transform);
                Destroy(gameObject, 1f);
            }
        }
    }
}
