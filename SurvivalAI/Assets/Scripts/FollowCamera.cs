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
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = hero.transform.position + offset;
	}
}
