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

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI foodText;

    [SerializeField] private TextMeshProUGUI woodCapacityText;
    [SerializeField] private TextMeshProUGUI stoneCapacityText;
    [SerializeField] private TextMeshProUGUI foodCapacityText;

    [SerializeField] private TextMeshProUGUI woodConsumptionText;
    [SerializeField] private TextMeshProUGUI stoneConsumptionText;
    [SerializeField] private TextMeshProUGUI foodConsumptionText;

    public ConstructionManager constructionManager;
    public WorkersManager workersManager;

    void Start () {
        resourcesAvailable.Add(ResourceTypes.Wood, 1000f);
        resourcesAvailable.Add(ResourceTypes.Stone, 1000f);
        resourcesAvailable.Add(ResourceTypes.Food, 1000f);

        resourcesCapacity.Add(ResourceTypes.Wood, 500f);
        resourcesCapacity.Add(ResourceTypes.Stone, 500f);
        resourcesCapacity.Add(ResourceTypes.Food, 500f);

        resourcesConsumption.Add(ResourceTypes.Wood, 0f);
        resourcesConsumption.Add(ResourceTypes.Stone, 0f);
        resourcesConsumption.Add(ResourceTypes.Food, 0f);

        InvokeRepeating("foodConsumption", 1, 1);
    }

    private void Update()
    {
        updateResourcesAvailable();

        woodText.text = resourcesAvailable[ResourceTypes.Wood].ToString();
        stoneText.text = resourcesAvailable[ResourceTypes.Stone].ToString();
        foodText.text = resourcesAvailable[ResourceTypes.Food].ToString();

        woodCapacityText.text = resourcesCapacity[ResourceTypes.Wood].ToString();
        stoneCapacityText.text = resourcesCapacity[ResourceTypes.Stone].ToString();
        foodCapacityText.text = resourcesCapacity[ResourceTypes.Food].ToString();

        woodConsumptionText.text = resourcesConsumption[ResourceTypes.Wood].ToString();
        stoneConsumptionText.text = resourcesConsumption[ResourceTypes.Stone].ToString();
        foodConsumptionText.text = resourcesConsumption[ResourceTypes.Food].ToString();

        updateResourcesComsumption();
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

    public void updateResourcesComsumption()
    {
        resourcesConsumption[ResourceTypes.Food] = 0f;
        resourcesConsumption[ResourceTypes.Wood] = 0f;
        resourcesConsumption[ResourceTypes.Stone] = 0f;

        foreach (WorkerBT w in workersManager.workers)
        {
            resourcesConsumption[ResourceTypes.Food] += 0.1f;
        }

        foreach(GameObject cb in constructionManager.constructionList)
        {
            resourcesConsumption[ResourceTypes.Wood] += cb.GetComponent<PlaceableBuilding>().neededWood;
            resourcesConsumption[ResourceTypes.Stone] += cb.GetComponent<PlaceableBuilding>().neededStone;
        }
        
    }
    void foodConsumption()
    {
        resourcesAvailable[ResourceTypes.Food] -= resourcesConsumption[ResourceTypes.Food];
    }

    void updateResourcesAvailable()
    {
        if(resourcesAvailable[ResourceTypes.Food] > resourcesCapacity[ResourceTypes.Food])
        {
            resourcesAvailable[ResourceTypes.Food] = resourcesCapacity[ResourceTypes.Food];
        }

        if (resourcesAvailable[ResourceTypes.Wood] > resourcesCapacity[ResourceTypes.Wood])
        {
            resourcesAvailable[ResourceTypes.Wood] = resourcesCapacity[ResourceTypes.Wood];
        }

        if (resourcesAvailable[ResourceTypes.Stone] > resourcesCapacity[ResourceTypes.Stone])
        {
            resourcesAvailable[ResourceTypes.Stone] = resourcesCapacity[ResourceTypes.Stone];
        }
    }

}
