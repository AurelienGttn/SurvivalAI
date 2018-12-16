using Boo.Lang;
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
    
    private TextMeshProUGUI woodAvailable;
    private TextMeshProUGUI woodCapacity;
    private TextMeshProUGUI woodConsumption;
    
    private TextMeshProUGUI stoneAvailable;
    private TextMeshProUGUI stoneCapacity;
    private TextMeshProUGUI stoneConsumption;

    private TextMeshProUGUI foodAvailable;
    private TextMeshProUGUI foodCapacity;
    private TextMeshProUGUI foodConsumption;

    // Food consumption variables
    private int workerCount;            // How many active workers are in game
    private int baseConsumption = 20;
    private float multiplier = 1.5f;
    private int workersConsumption;     // How much food those workers consume every minute
    private float currentFood;          // Keep the exact food quantity as a float
    private int cultureCount;           // How many culture buildings are built
    private int cultureEfficiency = 20; // How much food each culture gives

    private ConstructionManager constructionManager;
    private BuildingManager buildingManager;

    void Start () {
        #region resourcesSetup
        // Get all resource texts //
        woodAvailable = GameObject.Find("currentWood").GetComponent<TextMeshProUGUI>();
        stoneAvailable = GameObject.Find("currentStone").GetComponent<TextMeshProUGUI>();
        foodAvailable = GameObject.Find("currentFood").GetComponent<TextMeshProUGUI>();

        woodCapacity = GameObject.Find("maxWood").GetComponent<TextMeshProUGUI>();
        stoneCapacity = GameObject.Find("maxStone").GetComponent<TextMeshProUGUI>();
        foodCapacity = GameObject.Find("maxFood").GetComponent<TextMeshProUGUI>();

        woodConsumption = GameObject.Find("woodConsumption").GetComponent<TextMeshProUGUI>();
        stoneConsumption = GameObject.Find("stoneConsumption").GetComponent<TextMeshProUGUI>();
        foodConsumption = GameObject.Find("foodConsumption").GetComponent<TextMeshProUGUI>();
        ///////////////////////////

        resourcesAvailable.Add(ResourceTypes.Wood, 200);
        resourcesAvailable.Add(ResourceTypes.Stone, 200);

        resourcesCapacity.Add(ResourceTypes.Wood, 1000);
        resourcesCapacity.Add(ResourceTypes.Stone, 1000);

        resourcesConsumption.Add(ResourceTypes.Wood, 0.1f);
        resourcesConsumption.Add(ResourceTypes.Stone, 0.1f);
        

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

        StartCoroutine(ConsumeFood());
        #endregion

        buildingManager = FindObjectOfType<BuildingManager>();
    }

    private void Update()
    {
        // Game is lost if food goes below 0
        if (resourcesAvailable[ResourceTypes.Food] <= 0)
        {
            GameController.gameOver = true;
        }

        cultureCount = 0;
        if (buildingManager.constructedBuildings.Count > 0)
        {
            foreach (PlaceableBuilding c in buildingManager.constructedBuildings)
            {
                if (c.gameObject.name.StartsWith("BuildingCulture") && c.isConstructed == true)
                {
                    cultureCount++;
                }
            }
        }

        // Update food consumption
        workerCount = FindObjectOfType<WorkersManager>().workers.Count;
        workersConsumption = (baseConsumption * workerCount) - (cultureEfficiency * cultureCount) + (int)Mathf.Pow(workerCount, multiplier);
        resourcesConsumption[ResourceTypes.Food] = workersConsumption;


        woodAvailable.text = resourcesAvailable[ResourceTypes.Wood].ToString();
        stoneAvailable.text = resourcesAvailable[ResourceTypes.Stone].ToString();
        foodAvailable.text = resourcesAvailable[ResourceTypes.Food].ToString();

        woodCapacity.text = resourcesCapacity[ResourceTypes.Wood].ToString();
        stoneCapacity.text = resourcesCapacity[ResourceTypes.Stone].ToString();
        foodCapacity.text = resourcesCapacity[ResourceTypes.Food].ToString();

        woodConsumption.text = Mathf.Floor(resourcesConsumption[ResourceTypes.Wood]).ToString();
        stoneConsumption.text = Mathf.Floor(resourcesConsumption[ResourceTypes.Stone]).ToString();
        foodConsumption.text = Mathf.Floor(resourcesConsumption[ResourceTypes.Food]).ToString();
        
    }

    public void AddResource(ResourceTypes resourceType, int resourceQuantity)
    {
        if (resourcesAvailable[resourceType] >= resourcesCapacity[resourceType])
            return;
        if (resourceType == ResourceTypes.Wood)
            resourcesAvailable[ResourceTypes.Wood] += resourceQuantity;
        else if (resourceType == ResourceTypes.Stone)
            resourcesAvailable[ResourceTypes.Stone] += resourceQuantity;
        else if (resourceType == ResourceTypes.Food)
        {
            currentFood += resourceQuantity;
            resourcesAvailable[ResourceTypes.Food] += resourceQuantity;
        }
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
            currentFood -= workersConsumption / 60f;
            // Change the actual food available but keep it as an integer
            resourcesAvailable[ResourceTypes.Food] = Mathf.FloorToInt(currentFood);
        }
    }
}
