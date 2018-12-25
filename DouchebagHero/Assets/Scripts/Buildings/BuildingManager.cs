using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingTypes
{
    None,
    Forge,
    Culture,
    Warehouse,
    Tower
}

public class BuildingManager : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject[] fakeBuildings;
    public GameObject[] buildings;

    [Header("Construction management")]
    public List<Building> waitingForResources;
    public List<Building> waitingForWorkers;
    public List<Building> constructedBuildings;

    
    public Dictionary<BuildingTypes, string> buildingInfo = new Dictionary<BuildingTypes, string>();
    public Dictionary<BuildingTypes, int> necessaryWood = new Dictionary<BuildingTypes, int>();
    public Dictionary<BuildingTypes, int> necessaryStone = new Dictionary<BuildingTypes, int>();

    private void Start()
    {
        buildingInfo.Add(BuildingTypes.Forge, "Unlock research menu.");
        necessaryWood.Add(BuildingTypes.Forge, 200);
        necessaryStone.Add(BuildingTypes.Forge, 200);

        buildingInfo.Add(BuildingTypes.Culture, "Reduces food consumption by 20.");
        necessaryWood.Add(BuildingTypes.Culture, 150);
        necessaryStone.Add(BuildingTypes.Culture, 100);

        buildingInfo.Add(BuildingTypes.Warehouse, "Increases storage. Peasants will bring resources to closest warehouse.");
        necessaryWood.Add(BuildingTypes.Warehouse, 150);
        necessaryStone.Add(BuildingTypes.Warehouse, 150);

        buildingInfo.Add(BuildingTypes.Tower, "Shoots stones at nearby enemies.");
        necessaryWood.Add(BuildingTypes.Tower, 100);
        necessaryStone.Add(BuildingTypes.Tower, 100);
    }
}
