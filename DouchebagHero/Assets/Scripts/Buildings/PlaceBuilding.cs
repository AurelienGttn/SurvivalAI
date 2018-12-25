using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceBuilding : MonoBehaviour {

    private Camera mainCamera;
    private BuildingManager buildingManager;
    private Transform selectedBuilding;
    private Transform newBuilding;
    private int buildingType;

    
    void Start () {
        mainCamera = Camera.main;
        buildingManager = FindObjectOfType<BuildingManager>();
	}
	
	void Update () {
        #region Keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetBuilding(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetBuilding(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetBuilding(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetBuilding(3);
        }
        #endregion

        if (selectedBuilding != null)
        {
            // Cast a ray to place building on game plane
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane xy = new Plane(Vector3.up, new Vector3(0, 0, mainCamera.transform.position.y));
            float distance;
            xy.Raycast(mouseRay, out distance);
            Vector3 pointOnPlane = mouseRay.GetPoint(distance);

            selectedBuilding.position = pointOnPlane;

            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (CheckObstacles()) {
                    newBuilding = Instantiate(buildingManager.buildings[buildingType], selectedBuilding.position, Quaternion.identity, transform).transform;
                    Destroy(selectedBuilding.gameObject);

                    buildingManager.waitingForResources.Add(newBuilding.GetComponent<Building>());
                }
            }
        }
	}

    public void SetBuilding(int buildingIndex)
    {
        if (selectedBuilding != null)
        {
            Destroy(selectedBuilding.gameObject);
        }
        buildingType = buildingIndex;
        selectedBuilding = Instantiate(buildingManager.fakeBuildings[buildingIndex].transform);
    }

    private bool CheckObstacles()
    {
        return (selectedBuilding.GetComponent<FakeBuilding>().colliders == 0);
    }
}
