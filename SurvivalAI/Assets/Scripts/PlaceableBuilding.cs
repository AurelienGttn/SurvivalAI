using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    private bool isSelected;

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
