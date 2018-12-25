using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private Transform hero;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position;
        hero = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void LateUpdate () {
        // Check if the camer still has a targer
        // ie. hero is alive
        // Follow his position but keep the same offset
        if (hero)
            transform.position = hero.transform.position + offset;
	}
}
