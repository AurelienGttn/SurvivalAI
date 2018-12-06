using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    public List<GameObject> constructionList = new List<GameObject>();
    public List<GameObject> waitingList = new List<GameObject>();

    public ResourceManager resourceManager;
    public BuildingManager buildingManager;

    private void Start()
    {
       
    }

    public void Update()
    {
        attributeResources();
        switchToWaitingList();
        switchToConstructedList();
    }

    public void attributeResources()
    {
        foreach (GameObject go in constructionList)
        {
            while ((resourceManager.resourcesAvailable[ResourceTypes.Wood] >= 50) && (go.GetComponent<PlaceableBuilding>().attributedWood <= go.GetComponent<PlaceableBuilding>().neededWood - 50))
            {
                attributeWood(go, 50);
            }
            while((resourceManager.resourcesAvailable[ResourceTypes.Stone] >= 50) && (go.GetComponent<PlaceableBuilding>().attributedStone <= go.GetComponent<PlaceableBuilding>().neededStone - 50))
            {
                attributeStone(go, 50);
            }
        }
            

    }

    public void attributeWood(GameObject go, int wood)
    {
        go.GetComponent<PlaceableBuilding>().attributedWood += wood;
        resourceManager.RemoveResource(ResourceTypes.Wood, wood);
    }

    public void attributeStone(GameObject go, int stone)
    {
        go.GetComponent<PlaceableBuilding>().attributedStone += stone;
        resourceManager.RemoveResource(ResourceTypes.Stone, stone);
    }

    public void switchToWaitingList()
    {
        foreach (GameObject b in waitingList)
        {
            if (b.GetComponent<PlaceableBuilding>().checkAttributed())
            {
                waitingList.Add(b);
                constructionList.Remove(b);
            }
        }
    }

    public void switchToConstructedList()
    {
        foreach(GameObject b in waitingList)
        {
            if(b.GetComponent<PlaceableBuilding>().checkConstructionStatus())
            {
                buildingManager.constructedBuildings.Add(b);
                waitingList.Remove(b);
            }
        }
    }

    public void AddBuildingToConstructionList(GameObject g)
    {
        constructionList.Add(g);
    }
}
