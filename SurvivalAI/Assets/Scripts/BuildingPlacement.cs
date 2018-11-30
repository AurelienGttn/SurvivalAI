using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public float scrollSensitivity;

    public GameObject mainCamera;

    public ConstructionManager constructionManager;

    [HideInInspector]
    public GameObject currentSelectedBuilding;

    private PlaceableBuilding placeableBuilding;
    private Transform currentBuilding;
    private bool hasPlaced;

    public LayerMask buildingMask;

    private PlaceableBuilding placeableBuildingOld;

    void Update()
    {
        Vector3 m = Input.mousePosition;
        m = new Vector3(m.x, m.y, transform.position.y);
        Vector3 p = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(m);

        if (currentBuilding != null)
        {
            if(currentBuilding != null && !hasPlaced)
            {
                currentBuilding.position = new Vector3(p.x, 0, p.z);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(IsLegalPosition())
                {
                    hasPlaced = true;
                    constructionManager.GetComponent<ConstructionManager>().AddBuildingToConstructionList(currentBuilding.gameObject);
                    currentBuilding.gameObject.GetComponent<Renderer>().material = currentBuilding.gameObject.GetComponent<PlaceableBuilding>().normal;
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();
                Ray ray = new Ray(new Vector3(p.x, 8, p.z), Vector3.down);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, buildingMask))
                {
                    if (placeableBuildingOld != null)
                    {
                        placeableBuildingOld.SetSelected(false);
                    }
                    hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
                    placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();
                }
                else
                {
                    if(placeableBuildingOld != null)
                    {
                        placeableBuildingOld.SetSelected(false);
                    }
                }
            }
        }
    }

    bool IsLegalPosition()
    {
        if(placeableBuilding.colliders.Count > 0)
        {
            return false;
        }
        return true;
    }

    public void SetItem(GameObject b)
    {
        hasPlaced = false;
        currentBuilding = ((GameObject)Instantiate(b)).transform;
        currentSelectedBuilding = b;
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
    }
}
