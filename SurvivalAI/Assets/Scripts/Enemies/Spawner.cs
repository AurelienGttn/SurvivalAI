﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private int firstWaveTime;
    [SerializeField] private float spawnDelay;
    public Vector3 spawnZone;
    public Transform enemiesParent;
    private int waveNumber;
    
	void Start ()
    {
        InvokeRepeating("Spawn", firstWaveTime, spawnDelay);
        waveNumber = 1;
	}

    private void Spawn()
    {
        int spawnerIndex = Random.Range(0, spawnLocations.Length);
        Transform spawnLocation = spawnLocations[spawnerIndex];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPos = spawnLocation.position;
            if(spawnerIndex % 2 == 0)
                randomPos = new Vector3(randomPos.x + Random.Range(-spawnZone.x / 2, spawnZone.x / 2), 1, randomPos.z + Random.Range(-spawnZone.z / 2, spawnZone.z / 2));
            else
                randomPos = new Vector3(randomPos.x + Random.Range(-spawnZone.z / 2, spawnZone.z / 2), 1, randomPos.z + Random.Range(-spawnZone.x / 2, spawnZone.x / 2));
            GameObject newEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity, enemiesParent);
            newEnemy.GetComponent<HealthManager>().maxHealth *= waveNumber;
        }
        if (numberOfEnemies == 10)
        {
            waveNumber++;
            numberOfEnemies = 5;
        }
        else
        {
            numberOfEnemies++;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (i % 2 == 0)
                Gizmos.DrawWireCube(spawnLocations[i].position, spawnZone);
            else
                Gizmos.DrawWireCube(spawnLocations[i].position, new Vector3(spawnZone.z, 1, spawnZone.x));
        }
    }
}
