
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour {

    public List<WorkerBT> workers;
    public Dictionary<ResourceTypes, int> workersOccupation = new Dictionary<ResourceTypes, int>();

	// Use this for initialization
	void Start ()
    {
        workersOccupation.Add(ResourceTypes.Wood, 0);
        workersOccupation.Add(ResourceTypes.Stone, 0);
        workersOccupation.Add(ResourceTypes.Food, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
