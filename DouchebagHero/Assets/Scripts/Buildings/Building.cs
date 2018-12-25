using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Building : MonoBehaviour {

    public BuildingTypes buildingType;
    private BuildingManager buildingManager;
    private ResourceManager resourceManager;

    public bool isConstructed = false;
    public float health;
    private float maxHealth;
    
    public int neededWood;
    public int neededStone;
    public string infos;

    [SerializeField] private GameObject buildingUI;
    
    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        resourceManager = FindObjectOfType<ResourceManager>();

        // Information for tooltip
        switch (buildingType)
        {
            case BuildingTypes.Forge:
                infos = buildingManager.buildingInfo[BuildingTypes.Forge];
                neededWood = buildingManager.necessaryWood[BuildingTypes.Forge];
                neededStone = buildingManager.necessaryStone[BuildingTypes.Forge];
                maxHealth = 200;
                break;
            case BuildingTypes.Culture:
                infos = buildingManager.buildingInfo[BuildingTypes.Culture];
                neededWood = buildingManager.necessaryWood[BuildingTypes.Culture];
                neededStone = buildingManager.necessaryStone[BuildingTypes.Culture];
                maxHealth = 200;
                break;
            case BuildingTypes.Warehouse:
                infos = buildingManager.buildingInfo[BuildingTypes.Warehouse];
                neededWood = buildingManager.necessaryWood[BuildingTypes.Warehouse];
                neededStone = buildingManager.necessaryStone[BuildingTypes.Warehouse];
                maxHealth = 200;
                break;
            case BuildingTypes.Tower:
                infos = buildingManager.buildingInfo[BuildingTypes.Tower];
                neededWood = buildingManager.necessaryWood[BuildingTypes.Tower];
                neededStone = buildingManager.necessaryStone[BuildingTypes.Tower];
                maxHealth = 200;
                break;
            default:
                break;
        }

        GetComponent<HealthManager>().maxHealth = maxHealth;
        health = GetComponent<HealthManager>().currentHealth;

        resourceManager.resourcesConsumption[ResourceTypes.Wood] += neededWood;
        resourceManager.resourcesConsumption[ResourceTypes.Stone] += neededStone;
        
    }

    private void Update()
    {
        CheckAvailable();
        health = GetComponent<HealthManager>().currentHealth;
        CheckConstructionStatus();

        if (isConstructed == true)
        {
            Destroy(buildingUI);
        }
    }

    public void CheckAvailable(){
        if (buildingManager.waitingForResources.Count > 0)
        {
            if (buildingManager.waitingForResources[0] == this)
            {
                float reallyAvailableWood = resourceManager.resourcesAvailable[ResourceTypes.Wood];
                float reallyAvailableStone = resourceManager.resourcesAvailable[ResourceTypes.Stone];
                foreach (Building building in buildingManager.waitingForWorkers)
                {
                    reallyAvailableStone -= building.neededStone;
                    reallyAvailableWood -= building.neededWood;
                }
                if (reallyAvailableWood >= neededWood && reallyAvailableStone >= neededStone)
                {
                    buildingManager.waitingForWorkers.Add(this);
                    buildingManager.waitingForResources.Remove(this);
                }
            }
        }
    }

    public void CheckConstructionStatus()
    {
        if (!isConstructed && health == maxHealth)
        {
            isConstructed = true;
            buildingManager.constructedBuildings.Add(this);
            buildingManager.waitingForWorkers.Remove(this);

            resourceManager.RemoveResource(ResourceTypes.Wood, neededWood);
            resourceManager.RemoveResource(ResourceTypes.Stone, neededStone);
            resourceManager.resourcesConsumption[ResourceTypes.Wood] -= neededWood;
            resourceManager.resourcesConsumption[ResourceTypes.Stone] -= neededStone;

            Destroy(buildingUI);
        }
    }

    public void DestroyConstruction()
    {
        if (buildingManager.waitingForResources.Contains(this))
            buildingManager.waitingForResources.Remove(this);
        if (buildingManager.waitingForWorkers.Contains(this))
            buildingManager.waitingForWorkers.Remove(this);
        Destroy(gameObject);
    }
}
