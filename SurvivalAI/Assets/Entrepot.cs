using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrepot : MonoBehaviour {

    private bool alreadyUp = false;
    private ResourceManager resourceManager;

	// Use this for initialization
	void Start () {
        resourceManager = FindObjectOfType<ResourceManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(alreadyUp == false && GetComponent<PlaceableBuilding>().isConstructed == true)
        {
            alreadyUp = true;
            resourceManager.UpgradeCapacity(300);
        }
	}
}
