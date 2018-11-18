using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageFinder : MonoBehaviour
{

    private GameObject[] warehouses;
    private Transform mainBuilding;

    private void Start()
    {
        mainBuilding = GameObject.FindGameObjectWithTag("MainBuilding").transform;
    }

    private void Update()
    {
        warehouses = GameObject.FindGameObjectsWithTag("Warehouse");
    }

    public Transform FindClosest()
    {
        Transform closestWarehouse = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        Vector3 directionToTarget = mainBuilding.position - currentPosition;
        float dSqrToTarget = directionToTarget.sqrMagnitude;
        if (dSqrToTarget < closestDistance)
        {
            closestDistance = dSqrToTarget;
            closestWarehouse = mainBuilding;
        }

        for (int i = 0; i < warehouses.Length; i++)
        {
            Transform warehouse = warehouses[i].transform;
            directionToTarget = warehouse.position - currentPosition;
            dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                closestWarehouse = warehouse;
            }
        }

        return closestWarehouse;
    }
}