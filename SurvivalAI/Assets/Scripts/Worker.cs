using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    [SerializeField] private int maxResources = 3;
    private float woodGatheringTime = 2.0f, stoneGatheringTime = 2.0f;
    private bool isIdle, isGathering, isWalking;


	// Use this for initialization
	void Start () {
        isIdle = true;
        isGathering = false;
        isWalking = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateState(Vector3 targetPosition)
    {
        // Check what is the obstacle on the destination tile
        // If it's a resource, enter gathering state
        isWalking = false;
        isGathering = true;
        Debug.Log("arrived");
    }
}
