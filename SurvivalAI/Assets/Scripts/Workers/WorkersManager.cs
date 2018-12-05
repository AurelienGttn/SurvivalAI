
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour {

    public List<WorkerBT> workers;
    public Dictionary<ResourceTypes, int> gatheringWorkers = new Dictionary<ResourceTypes, int>();
    public int buildingWorkers;

	// Use this for initialization
	void Start ()
    {
        gatheringWorkers.Add(ResourceTypes.Wood, 0);
        gatheringWorkers.Add(ResourceTypes.Stone, 0);
        gatheringWorkers.Add(ResourceTypes.Food, 0);

        buildingWorkers = 0;
    }
}
