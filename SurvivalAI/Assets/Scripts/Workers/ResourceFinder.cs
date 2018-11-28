using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder : MonoBehaviour {

    private GameObject[] trees;
    private GameObject[] rocks;
    private GameObject[] berries;

    private void Awake()
    {
    }
    
    private void Update()
    {
        trees = GameObject.FindGameObjectsWithTag("Wood");
        rocks = GameObject.FindGameObjectsWithTag("Stone");
        berries = GameObject.FindGameObjectsWithTag("Food");
    }

    public Transform FindClosest(ResourceTypes resourceType)
    {
        Transform closestResource = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if (resourceType == ResourceTypes.Wood)
        {
            for (int i = 0; i < trees.Length; i++)
            {
                Transform tree = trees[i].transform;
                Vector3 directionToTarget = tree.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistance)
                {
                    closestDistance = dSqrToTarget;
                    closestResource = tree;
                }
            }
        }
        else if (resourceType == ResourceTypes.Stone)
        {
            for (int i = 0; i < rocks.Length; i++)
            {
                Transform rock = rocks[i].transform;
                Vector3 directionToTarget = rock.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistance)
                {
                    closestDistance = dSqrToTarget;
                    closestResource = rock;
                }
            }
        }
        else if (resourceType == ResourceTypes.Food)
        {
            for (int i = 0; i < berries.Length; i++)
            {
                Transform berry = berries[i].transform;
                Vector3 directionToTarget = berry.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistance)
                {
                    closestDistance = dSqrToTarget;
                    closestResource = berry;
                }
            }
        }

        return closestResource;
    }
}
