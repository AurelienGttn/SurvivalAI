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

        buildingInfo.Add(BuildingTypes.Forge, "Make available researchs' menu");
        necessaryWood.Add(BuildingTypes.Forge, 200);
        necessaryStone.Add(BuildingTypes.Forge, 200);

        buildingInfo.Add(BuildingTypes.Culture, "Make grow food and give a low increase of food each seconds");
        necessaryWood.Add(BuildingTypes.Culture, 150);
        necessaryStone.Add(BuildingTypes.Culture, 100);

        buildingInfo.Add(BuildingTypes.Entrepot, "It's where peasants bring back gathered resources, it also increase maximum resources");
        necessaryWood.Add(BuildingTypes.Entrepot, 150);
        necessaryStone.Add(BuildingTypes.Entrepot, 150);

        buildingInfo.Add(BuildingTypes.Tower, "Shoot stones on nearby enemies");
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
