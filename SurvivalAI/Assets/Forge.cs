using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour {

    public GameObject text;
    public GameObject researchUI;

    private void Update()
    {
        if(this.GetComponent<PlaceableBuilding>().isConstructed)
        {
            text.SetActive(false);
            researchUI.SetActive(true);
        }

    }
}
