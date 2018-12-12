using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour {

    private GameObject unavailableText;
    private GameObject researchUI;
    private int upgradeCount;

    private void Start()
    {
        unavailableText = GameObject.Find("Instructions");
        researchUI = GameObject.Find("ResearchOptions");
        upgradeCount = researchUI.transform.childCount;
    }

    private void Update()
    {
        if(GetComponent<PlaceableBuilding>().isConstructed)
        {
            unavailableText.SetActive(false);
            for (int i = 0; i < upgradeCount; i++)
            {
                researchUI.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

    }
}
