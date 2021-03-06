﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [SerializeField] private ParticleSystem deathAnimation;
    public float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private Slider healthBar;
    private bool isDead = false;

    private Renderer[] m_renderer;

    private ResourceFinder resourceFinder;
    
	void Start ()
    {
        if (tag == "Building" || tag == "Warehouse")
            currentHealth = 1;
        else {
            currentHealth = maxHealth;
            if (tag == "Player" || tag == "Worker")
                StartCoroutine(RegenHealth());
        }
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        m_renderer = GetComponentsInChildren<Renderer>();
        resourceFinder = FindObjectOfType<ResourceFinder>();
	}

    private void Update()
    {
        //Regen over time
        //if (tag != "Building" && tag != "Warehouse")
        //{
        //    Heal(0.05f);
        //}
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Check if the animation has already been played
            if (!isDead)
            {
                if (tag == "Worker")
                {
                    FindObjectOfType<WorkersManager>().workers.Remove(GetComponent<WorkerBT>());
                }
                foreach (Renderer renderer in m_renderer)
                {
                    renderer.enabled = false;
                }
                Instantiate(deathAnimation, gameObject.transform);
                Destroy(gameObject, 0.7f);
            }
            isDead = true;

            // If it is a resource, we need to remove it from
            // the list of resources so it can't be found anymore
            if (tag == "Wood")
            {
                resourceFinder.trees.Remove(transform);
            }
            else if (tag == "Stone")
            {
                resourceFinder.rocks.Remove(transform);
            }
            else if (tag == "Food")
            {
                resourceFinder.berries.Remove(transform);
            }

            // Game is over is main building is destroyed or if hero is dead
            if (tag == "Player" || tag == "MainBuilding")
            {
                GameController.gameOver = true;
            }
        }
    }

    public void Heal(float healValue)
    {
        currentHealth += healValue;
        if (healthBar)
            healthBar.value = currentHealth;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Regen 4% health every second
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(1);

        Heal(maxHealth * 0.04f);
        StartCoroutine(RegenHealth());
    }
}
