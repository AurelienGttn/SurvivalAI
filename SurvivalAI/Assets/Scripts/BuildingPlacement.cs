using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {
    
    public Camera mainCamera;

    public ConstructionManager constructionManager;

    private PlaceableBuilding placeableBuilding;
    private Transform currentBuilding;
    private bool hasPlaced;

    public LayerMask buildingMask;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, mainCamera.transform.position.y));
        float distance;
        xy.Raycast(mouseRay, out distance);
        Vector3 p = mouseRay.GetPoint(distance);

        if (currentBuilding != null)
        {
            if(!hasPlaced)
            {
                currentBuilding.position = p;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(IsLegalPosition())
                {
                    hasPlaced = true;
                    currentBuilding.gameObject.GetComponent<Renderer>().material = currentBuilding.gameObject.GetComponent<PlaceableBuilding>().normal;
                }
            }
        }
    }

    bool IsLegalPosition()
    {
        return placeableBuilding.colliders.Count == 0;
    }

    public void SetItem(GameObject b)
    {
        hasPlaced = false;
        currentBuilding = Instantiate(b).transform;
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
        constructionManager.GetComponent<ConstructionManager>().AddBuildingToConstructionList(placeableBuilding);
    }
}
