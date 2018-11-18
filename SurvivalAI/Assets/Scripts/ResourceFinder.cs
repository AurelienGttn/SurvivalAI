using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder : MonoBehaviour {

    private GameObject[] trees;
    private GameObject[] rocks;
    private GameObject[] berries;

    private void Update()
    {
        trees = GameObject.FindGameObjectsWithTag("Wood");
        rocks = GameObject.FindGameObjectsWithTag("Rock");
        berries = GameObject.FindGameObjectsWithTag("Berry");
    }

    public Transform FindClosest(string resourceType)
    {
        Transform closestResource = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if (resourceType == "Wood")
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
        else if (resourceType == "Rock")
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
        else if (resourceType == "Berry")
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

    public GameObject[] getTrees()
    {
        return trees;
    }

    public GameObject[] getRocks()
    {
        return rocks;
    }

    public GameObject[] getBerries()
    {
        return berries;
    }
}
