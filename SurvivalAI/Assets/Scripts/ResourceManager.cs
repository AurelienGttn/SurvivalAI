using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public int woodAvailable, stoneAvailable, foodAvailable;
    public int woodCapacity, stoneCapacity, foodCapacity;

	void Start () {
        woodCapacity = 10;
        stoneCapacity = 10;
        foodCapacity = 10;

        woodAvailable = 0;
        stoneAvailable = 0;
        foodAvailable = 0;
	}

    public void AddResource(string resourceType, int resourceQuantity)
    {
        if (resourceType == "Wood")
            woodAvailable += resourceQuantity;
        else if (resourceType == "Stone")
            stoneAvailable += resourceQuantity;
        else if (resourceType == "Food")
            foodAvailable += resourceQuantity;

        Debug.Log("wood = " + woodAvailable);
        Debug.Log("stone = " + stoneAvailable);
        Debug.Log("food = " + foodAvailable);
    }

    public void RemoveResource(string resourceType, int resourceQuantity)
    {
        if (resourceType == "Wood")
            woodAvailable -= resourceQuantity;
        else if (resourceType == "Stone")
            stoneAvailable -= resourceQuantity;
        else if (resourceType == "Food")
            foodAvailable -= resourceQuantity;
    }

    public void UpgradeCapacity(int capacityIncrease)
    {
        woodCapacity += capacityIncrease;
        stoneCapacity += capacityIncrease;
        foodCapacity += capacityIncrease;
    }
}
