using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    public List<PlaceableBuilding> constructionList = new List<PlaceableBuilding>();
    public List<PlaceableBuilding> waitingList = new List<PlaceableBuilding>();

    
    public void AddBuildingToConstructionList(PlaceableBuilding building)
    {
        constructionList.Add(building);
    }
}
