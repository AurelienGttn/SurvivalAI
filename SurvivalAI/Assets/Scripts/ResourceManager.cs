using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public int woodAvailable, stoneAvailable, foodAvailable;
    public int woodCapacity, stoneCapacity, foodCapacity;

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI foodText;

    void Start () {
        woodCapacity = 10;
        stoneCapacity = 10;
        foodCapacity = 10;

        woodAvailable = 0;
        stoneAvailable = 0;
        foodAvailable = 0;
	}

    private void Update()
    {
        woodText.text = woodAvailable.ToString();
        stoneText.text = stoneAvailable.ToString();
        foodText.text = foodAvailable.ToString();
    }

    public void AddResource(string resourceType, int resourceQuantity)
    {
        if (resourceType == "Wood")
            woodAvailable += resourceQuantity;
        else if (resourceType == "Stone")
            stoneAvailable += resourceQuantity;
        else if (resourceType == "Food")
            foodAvailable += resourceQuantity;
    }

    public void RemoveResource(string resourceType, int resourceQuantity)
    {
        if (resourceType == "Wood")
            woodAvailable -= resourceQuantity;
        else if (resourceType == "Stone")
            stoneAvailable -= resourceQuantity;
        else if (resourceType == "Food")
            foodAvailable -= resourceQuantity;
    }

    public void UpgradeCapacity(int capacityIncrease)
    {
        woodCapacity += capacityIncrease;
        stoneCapacity += capacityIncrease;
        foodCapacity += capacityIncrease;
    }
}
