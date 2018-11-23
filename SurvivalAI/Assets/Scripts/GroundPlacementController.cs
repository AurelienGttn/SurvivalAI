using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour {

    [SerializeField]
    private GameObject[] placeableBuildings;

    public int buildingID;

    private GameObject currentPlaceableBuilding;
	
	// Update is called once per frame
	void Update ()
    {
        HandleNewObject();
		if(currentPlaceableBuilding != null)
        {
            MoveCurrentPlaceableBuildingToMouse();
        }
	}

    private void MoveCurrentPlaceableBuildingToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)).point;
        {
            currentPlaceableBuilding.transform.position = hitInfo.point;
        }
    }

    public void HandleNewObject()
    {
        if(currentPlaceableBuilding == null)
        {
            currentPlaceableBuilding = Instantiate(placeableBuildings[buildingID]);
        }
        else
        {
            Destroy
        }

    }
}
