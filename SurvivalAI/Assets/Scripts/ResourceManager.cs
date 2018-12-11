﻿using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ResourceTypes
{
    None,
    Wood,
    Food,
    Stone
}

public class ResourceManager : MonoBehaviour {

    public Dictionary<ResourceTypes, float> resourcesAvailable = new Dictionary<ResourceTypes, float>();
    public Dictionary<ResourceTypes, float> resourcesCapacity = new Dictionary<ResourceTypes, float>();
    public Dictionary<ResourceTypes, float> resourcesConsumption = new Dictionary<ResourceTypes, float>();

    [Header("Wood")]
    [SerializeField] private TextMeshProUGUI woodAvailable;
    [SerializeField] private TextMeshProUGUI woodCapacity;
    [SerializeField] private TextMeshProUGUI woodConsumption;

    [Header("Stone")]
    [SerializeField] private TextMeshProUGUI stoneAvailable;
    [SerializeField] private TextMeshProUGUI stoneCapacity;
    [SerializeField] private TextMeshProUGUI stoneConsumption;

    [Header("Food")]
    [SerializeField] private TextMeshProUGUI foodAvailable;
    [SerializeField] private TextMeshProUGUI foodCapacity;
    [SerializeField] private TextMeshProUGUI foodConsumption;

    private int workerCount;            // How many active workers are in game
    private int baseConsumption = 20;
    private int multiplier = 3;
    private int workersConsumption;     // How much food those workers consume every minute
    private float currentFood;          // Keep the exact food quantity as a float
    private int cultureCount;           // How many constructed culture

    public ConstructionManager constructionManager;
    public BuildingManager buildingManager;

    void Start () {
        resourcesAvailable.Add(ResourceTypes.Wood, 1000);
        resourcesAvailable.Add(ResourceTypes.Stone, 1000);

        resourcesCapacity.Add(ResourceTypes.Wood, 1000);
        resourcesCapacity.Add(ResourceTypes.Stone, 1000);

        resourcesConsumption.Add(ResourceTypes.Wood, 100);
        resourcesConsumption.Add(ResourceTypes.Stone, 100);
        

        // Food consumption is special because it is not needed
        // to build but to keep workers alive. It reduces a little
        // every second and the player always needs to have more than 0
        workerCount = FindObjectOfType<WorkersManager>().workers.Count;

        // All workers don't always consume the same amount of food 
        // through the game: the more workers you have, the more
        // food each will consume
        workersConsumption = baseConsumption + (int)Mathf.Pow(workerCount, multiplier);
        resourcesConsumption.Add(ResourceTypes.Food, workersConsumption);

        // As food is always needed, the player needs to have some
        // at the very beginning, else he would lose before playing
        resourcesAvailable.Add(ResourceTypes.Food, 100);
        currentFood = resourcesAvailable[ResourceTypes.Food];
        resourcesCapacity.Add(ResourceTypes.Food, 1000);
    }

    private void Update()
    {
        cultureCount = 0;
        foreach(PlaceableBuilding c in buildingManager.constructedBuildings)
        {
            if (c.gameObject.name == "BuildingCulture" && c.isConstructed == true)
            {
                cultureCount += 1;
            }
        }

        // Update food consumption
        workerCount = FindObjectOfType<WorkersManager>().workers.Count;
        workersConsumption = baseConsumption - 5 * cultureCount + (int)Mathf.Pow(workerCount, multiplier);
        resourcesConsumption[ResourceTypes.Food] = workersConsumption;


        woodAvailable.text = resourcesAvailable[ResourceTypes.Wood].ToString();
        stoneAvailable.text = resourcesAvailable[ResourceTypes.Stone].ToString();
        foodAvailable.text = resourcesAvailable[ResourceTypes.Food].ToString();

        woodCapacity.text = resourcesCapacity[ResourceTypes.Wood].ToString();
        stoneCapacity.text = resourcesCapacity[ResourceTypes.Stone].ToString();
        foodCapacity.text = resourcesCapacity[ResourceTypes.Food].ToString();

        woodConsumption.text = resourcesConsumption[ResourceTypes.Wood].ToString();
        stoneConsumption.text = resourcesConsumption[ResourceTypes.Stone].ToString();
        foodConsumption.text = resourcesConsumption[ResourceTypes.Food].ToString();
        
    }

    public void AddResource(ResourceTypes resourceType, int resourceQuantity)
    {
        if (resourceType == ResourceTypes.Wood)
            resourcesAvailable[ResourceTypes.Wood] += resourceQuantity;
        else if (resourceType == ResourceTypes.Stone)
            resourcesAvailable[ResourceTypes.Stone] += resourceQuantity;
        else if (resourceType == ResourceTypes.Food)
            resourcesAvailable[ResourceTypes.Food] += resourceQuantity;
    }

    public void RemoveResource(ResourceTypes resourceType, int resourceQuantity)
    {
        if (resourceType == ResourceTypes.Wood)
            resourcesAvailable[ResourceTypes.Wood] -= resourceQuantity;
        else if (resourceType == ResourceTypes.Stone)
            resourcesAvailable[ResourceTypes.Stone] -= resourceQuantity;
        else if (resourceType == ResourceTypes.Food)
            resourcesAvailable[ResourceTypes.Food] -= resourceQuantity;
    }

    public void UpgradeCapacity(int capacityIncrease)
    {
        resourcesCapacity[ResourceTypes.Wood] += capacityIncrease;
        resourcesCapacity[ResourceTypes.Stone] += capacityIncrease;
        resourcesCapacity[ResourceTypes.Food] += capacityIncrease;
    }

    // Reduce food available every second based on the workers consumption
    private IEnumerator ConsumeFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            // Store the exact food value
            currentFood -= workersConsumption / 60;
            // Change the actual food available but keep it as an integer
            resourcesAvailable[ResourceTypes.Food] = Mathf.FloorToInt(currentFood);
        }
    }
}
