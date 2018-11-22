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

    private void OnGUI()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            buildingPlacement.SetItem(buildings[i]);
        }
    }
}
