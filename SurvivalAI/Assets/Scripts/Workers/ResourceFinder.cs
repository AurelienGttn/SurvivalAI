using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder : MonoBehaviour {

    private GameObject[] treeArray;
    private List<Transform> trees = new List<Transform>();
    private GameObject[] rockArray;
    private List<Transform> rocks = new List<Transform>();
    private GameObject[] berryArray;
    private List<Transform> berries = new List<Transform>();

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
        Debug.Log("current pos = " + currentPosition);

        if (resourceType == ResourceTypes.Wood)
        {
            foreach (Transform tree in trees)
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
        else if (resourceType == ResourceTypes.Stone)
        {
            foreach (Transform rock in rocks)
            {
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
            foreach (Transform berry in berries)
            {
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
