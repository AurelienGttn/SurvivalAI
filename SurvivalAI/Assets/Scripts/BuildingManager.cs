using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] buildings;
    private BuildingPlacement buildingPlacement;

    private void Start()
    {
        buildingPlacement = GetComponent<BuildingPlacement>();
    }

    private void Update()
    {
        
    }

    public void OnButtonSetBuilding(int ID)
    {
        buildingPlacement.SetItem(buildings[ID]);
    }
}
