using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {

    private NavMeshAgent agent;
    private Worker worker;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        worker = GetComponent<Worker>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }

        // if the worker is arrived, change its state
        Debug.Log("distToArrival" + Vector3.Distance(agent.destination, agent.transform.position));
        Debug.Log("stopDistance" + agent.stoppingDistance);
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            Debug.Log("here");
            worker.UpdateState(agent.destination);
        }
	}
}
