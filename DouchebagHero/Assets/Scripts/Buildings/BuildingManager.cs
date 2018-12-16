using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] buildings;
    private BuildingPlacement buildingPlacement;

    public List<PlaceableBuilding> constructedBuildings;

    public enum BuildingTypes
    {
        Forge,
        Culture,
        Entrepot,
        Tower
    }

    public Dictionary<BuildingTypes, string> buildingInfo = new Dictionary<BuildingTypes, string>();
    public Dictionary<BuildingTypes, int> necessaryWood = new Dictionary<BuildingTypes, int>();
    public Dictionary<BuildingTypes, int> necessaryStone = new Dictionary<BuildingTypes, int>();

    private void Start()
    {
        buildingPlacement = GetComponent<BuildingPlacement>();

        buildingInfo.Add(BuildingTypes.Forge, "Unlock research menu.");
        necessaryWood.Add(BuildingTypes.Forge, 200);
        necessaryStone.Add(BuildingTypes.Forge, 200);

        buildingInfo.Add(BuildingTypes.Culture, "Reduces food consumption by 20.");
        necessaryWood.Add(BuildingTypes.Culture, 150);
        necessaryStone.Add(BuildingTypes.Culture, 100);

        buildingInfo.Add(BuildingTypes.Entrepot, "Increases storage. Peasants will bring resources to closest warehouse.");
        necessaryWood.Add(BuildingTypes.Entrepot, 150);
        necessaryStone.Add(BuildingTypes.Entrepot, 150);

        buildingInfo.Add(BuildingTypes.Tower, "Shoots stones at nearby enemies.");
        necessaryWood.Add(BuildingTypes.Tower, 100);
        necessaryStone.Add(BuildingTypes.Tower, 100);
    }

    private void Update()
    {
        
    }

    public void OnButtonSetBuilding(int ID)
    {
        buildingPlacement.SetItem(buildings[ID]);
    }
}
