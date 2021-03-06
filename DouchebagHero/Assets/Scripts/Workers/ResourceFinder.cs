﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder : MonoBehaviour {

    private GameObject[] treeArray;
    private GameObject[] rockArray;
    private GameObject[] berryArray;
    [HideInInspector]
    public List<Transform> trees = new List<Transform>();
    [HideInInspector]
    public List<Transform> rocks = new List<Transform>();
    [HideInInspector]
    public List<Transform> berries = new List<Transform>();

    private void Start()
    {
        treeArray = GameObject.FindGameObjectsWithTag("Wood");
        for (int i = 0; i < treeArray.Length; i++)
        {
            trees.Add(treeArray[i].transform);
        }
        rockArray = GameObject.FindGameObjectsWithTag("Stone");
        for (int i = 0; i < rockArray.Length; i++)
        {
            rocks.Add(rockArray[i].transform);
        }
        berryArray = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < berryArray.Length; i++)
        {
            berries.Add(berryArray[i].transform);
        }
    }
    
    private void Update()
    {

    }

    public Transform FindClosest(ResourceTypes resourceType)
    {
        Transform closestResource = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if (resourceType == ResourceTypes.Wood)
        {
            foreach (Transform tree in trees)
            {
                //Check if it still exists
                if (tree != null)
                {
                    Vector3 directionToTarget = tree.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistance)
                    {
                        closestDistance = dSqrToTarget;
                        closestResource = tree;
                    }
                }
            }
        }
        else if (resourceType == ResourceTypes.Stone)
        {
            foreach (Transform rock in rocks)
            {
                if (rock != null)
                {
                    //Check if it still exists
                    Vector3 directionToTarget = rock.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistance)
                    {
                        closestDistance = dSqrToTarget;
                        closestResource = rock;
                    }
                }
            }
        }
        else if (resourceType == ResourceTypes.Food)
        {
            foreach (Transform berry in berries)
            {
                if (berry != null)
                {
                    //Check if it still exists
                    Vector3 directionToTarget = berry.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistance)
                    {
                        closestDistance = dSqrToTarget;
                        closestResource = berry;
                    }
                }
            }
        }

        return closestResource;
    }
}
