using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.destination = hit.point;
            }
        }
	}
}
