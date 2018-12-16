using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    private GameObject buildingsParent;

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    public BuildingManager.BuildingTypes type;
    public ConstructionManager constructionManager;
    private BuildingManager buildingManager;
    private ResourceManager resourceManager;

    public bool isConstructed = false;
    public float health;
    private float maxHealth;

    public Material normal;
    public Material fade;
    
    public int neededWood;
    public int neededStone;
    public string infos;

    [SerializeField] private GameObject buildingUI;
    
    private void Start()
    {
        buildingsParent = GameObject.Find("Buildings");
        transform.parent = buildingsParent.transform;

        constructionManager = FindObjectOfType<ConstructionManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        resourceManager = FindObjectOfType<ResourceManager>();

        switch (type)
        {
            case BuildingManager.BuildingTypes.Forge:
                infos = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Forge];
                neededWood = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Forge];
                neededStone = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Forge];
                maxHealth = 200;
                break;
            case BuildingManager.BuildingTypes.Culture:
                infos = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Forge];
                neededWood = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Culture];
                neededStone = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Culture];
                maxHealth = 200;
                break;
            case BuildingManager.BuildingTypes.Entrepot:
                infos = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Forge];
                neededWood = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Entrepot];
                neededStone = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Entrepot];
                maxHealth = 200;
                break;
            case BuildingManager.BuildingTypes.Tower:
                infos = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Forge];
                neededWood = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Tower];
                neededStone = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Tower];
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
        if (constructionManager.constructionList.Count > 0)
        {
            if (constructionManager.constructionList[0] == this)
            {
                float reallyAvailableWood = resourceManager.resourcesAvailable[ResourceTypes.Wood];
                float reallyAvailableStone = resourceManager.resourcesAvailable[ResourceTypes.Stone];
                foreach (PlaceableBuilding building in constructionManager.waitingList)
                {
                    reallyAvailableStone -= building.neededStone;
                    reallyAvailableWood -= building.neededWood;
                }
                if (reallyAvailableWood >= neededWood && reallyAvailableStone >= neededStone)
                {
                    constructionManager.waitingList.Add(this);
                    constructionManager.constructionList.Remove(this);
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
            constructionManager.waitingList.Remove(this);

            resourceManager.RemoveResource(ResourceTypes.Wood, neededWood);
            resourceManager.RemoveResource(ResourceTypes.Stone, neededStone);
            resourceManager.resourcesConsumption[ResourceTypes.Wood] -= neededWood;
            resourceManager.resourcesConsumption[ResourceTypes.Stone] -= neededStone;

            Destroy(buildingUI);
        }
    }

    public void DestroyConstruction()
    {
        if (constructionManager.waitingList.Contains(this))
            constructionManager.waitingList.Remove(this);
        if (constructionManager.constructionList.Contains(this))
            constructionManager.constructionList.Remove(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Building")
        {
            colliders.Add(c);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Building")
        {
            colliders.Remove(c);
        }
    }
}
