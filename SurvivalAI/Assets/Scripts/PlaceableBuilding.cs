using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    private GameObject buildingsParent;

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    public BuildingTypes type;
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

    [SerializeField] private GameObject buildingUI;

    public enum BuildingTypes
    {
        Forge,
        Culture,
        Mine,
        Entrepot,
        House,
        Tower
    }

    private void Start()
    {
        buildingsParent = GameObject.Find("Buildings");
        transform.parent = buildingsParent.transform;

        constructionManager = FindObjectOfType<ConstructionManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        resourceManager = FindObjectOfType<ResourceManager>();

        switch (type)
        {
            case BuildingTypes.Forge:
                neededWood = 200;
                neededStone = 200;
                maxHealth = 200;
                break;
            case BuildingTypes.Culture:
                neededWood = 150;
                neededStone = 100;
                maxHealth = 200;
                break;
            case BuildingTypes.Mine:
                neededWood = 100;
                neededStone = 150;
                maxHealth = 200;
                break;
            case BuildingTypes.Entrepot:
                neededWood = 150;
                neededStone = 150;
                maxHealth = 200;
                break;
            case BuildingTypes.House:
                neededWood = 300;
                neededStone = 300;
                maxHealth = 500;
                break;
            case BuildingTypes.Tower:
                neededWood = 100;
                neededStone = 100;
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
                if (resourceManager.resourcesAvailable[ResourceTypes.Wood] >= neededWood
                    && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= neededStone)
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
