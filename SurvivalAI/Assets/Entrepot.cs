using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrepot : MonoBehaviour {

    private bool alreadyUp = false;
    public ResourceManager resourceManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(alreadyUp == false && this.GetComponent<PlaceableBuilding>().isConstructed == true)
        {
            alreadyUp = true;
            resourceManager.UpgradeCapacity(300);
        }
	}
}
