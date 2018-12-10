using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public float scrollSensitivity;

    public Camera mainCamera;

    public ConstructionManager constructionManager;

    [HideInInspector]
    public GameObject currentSelectedBuilding;

    private PlaceableBuilding placeableBuilding;
    private Transform currentBuilding;
    private bool hasPlaced;

    public LayerMask buildingMask;

    private PlaceableBuilding placeableBuildingOld;

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
                    //constructionManager.GetComponent<ConstructionManager>().AddBuildingToConstructionList(currentBuilding.gameObject);
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
        return placeableBuilding.colliders.Count == 0;
    }

    public void SetItem(GameObject b)
    {
        hasPlaced = false;
        currentBuilding = Instantiate(b).transform;
        currentSelectedBuilding = b;
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
        constructionManager.GetComponent<ConstructionManager>().AddBuildingToConstructionList(placeableBuilding);
    }
}
