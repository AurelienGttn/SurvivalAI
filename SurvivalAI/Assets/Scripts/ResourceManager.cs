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

    public Dictionary<ResourceTypes, int> resourcesAvailable = new Dictionary<ResourceTypes, int>();
    public Dictionary<ResourceTypes, int> resourcesCapacity = new Dictionary<ResourceTypes, int>();
    public Dictionary<ResourceTypes, int> resourcesNeeded = new Dictionary<ResourceTypes, int>();

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI foodText;

    void Start () {
        resourcesAvailable.Add(ResourceTypes.Wood, 200);
        resourcesAvailable.Add(ResourceTypes.Stone, 200);
        resourcesAvailable.Add(ResourceTypes.Food, 200);

        resourcesCapacity.Add(ResourceTypes.Wood, 1000);
        resourcesCapacity.Add(ResourceTypes.Stone, 1000);
        resourcesCapacity.Add(ResourceTypes.Food, 1000);

        resourcesNeeded.Add(ResourceTypes.Wood, 700);
        resourcesNeeded.Add(ResourceTypes.Stone, 300);
        resourcesNeeded.Add(ResourceTypes.Food, 300);
	}

    private void Update()
    {
        woodText.text = resourcesAvailable[ResourceTypes.Wood].ToString();
        stoneText.text = resourcesAvailable[ResourceTypes.Stone].ToString();
        foodText.text = resourcesAvailable[ResourceTypes.Food].ToString();
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
}
