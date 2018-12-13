
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour {

    public List<WorkerBT> workers;
    public Dictionary<ResourceTypes, int> gatheringWorkers = new Dictionary<ResourceTypes, int>();
    public int foodWorkers, woodWorkers, stoneWorkers;
    public int buildingWorkers;

	// Use this for initialization
	void Start ()
    {
        gatheringWorkers.Add(ResourceTypes.Wood, 0);
        gatheringWorkers.Add(ResourceTypes.Stone, 0);
        gatheringWorkers.Add(ResourceTypes.Food, 0);

        buildingWorkers = 0;
    }

    private void Update()
    {
        foodWorkers = gatheringWorkers[ResourceTypes.Food];
        woodWorkers = gatheringWorkers[ResourceTypes.Wood];
        stoneWorkers = gatheringWorkers[ResourceTypes.Stone];
        buildingWorkers = Mathf.Clamp(buildingWorkers, 0, workers.Count);
        gatheringWorkers[ResourceTypes.Wood] = Mathf.Clamp(gatheringWorkers[ResourceTypes.Wood], 0, workers.Count);
        gatheringWorkers[ResourceTypes.Stone] = Mathf.Clamp(gatheringWorkers[ResourceTypes.Stone], 0, workers.Count);
        gatheringWorkers[ResourceTypes.Food] = Mathf.Clamp(gatheringWorkers[ResourceTypes.Food], 0, workers.Count);
    }
}
