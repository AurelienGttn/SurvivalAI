using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    private GameObject buildingsParent;

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    public BuildingTypes type;

    public Material normal;
    public Material fade;

    public int attributedWood;
    public int attributedStone;

    public int neededWood;
    public int neededStone;

    private bool isSelected;

    public enum BuildingTypes
    {
        Forge,
        Culture,
        Mine,
        Entrepot,
        House,
        Tower
    }

    private void Start()
    {
        buildingsParent = GameObject.Find("Buildings");
        transform.parent = buildingsParent.transform;

        attributedWood = 0;
        attributedStone = 0;
        switch (type)
        {
            case BuildingTypes.Forge:
                neededWood = 200;
                neededStone = 200;
                break;
            case BuildingTypes.Culture:
                neededWood = 100;
                neededStone = 50;
                break;
            case BuildingTypes.Mine:
                neededWood = 50;
                neededStone = 100;
                break;
            case BuildingTypes.Entrepot:
                neededWood = 150;
                neededStone = 150;
                break;
            case BuildingTypes.House:
                neededWood = 300;
                neededStone = 300;
                break;
            case BuildingTypes.Tower:
                neededWood = 100;
                neededStone = 100;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        Debug.Log(attributedWood);
        Debug.Log(attributedStone);
        checkAttributed();
    }

    public bool checkAttributed()
    {
        if(neededWood == attributedWood && neededStone == attributedStone)
        {
            Debug.Log("Assez de ressources");
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Building")
        {
            colliders.Add(c);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Building")
        {
            colliders.Remove(c);
        }
    }

    public void SetSelected(bool s)
    {
        isSelected = s;
    }
}
