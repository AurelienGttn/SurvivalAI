using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBuilding : MonoBehaviour {

    public int colliders;

    // Use this for initialization
    void Start () {
        colliders = 0;
	}

    void OnCollisionEnter(Collision collision)
    {
        colliders++;
    }

    void OnCollisionExit(Collision collision)
    {
        colliders--;
    }
}
