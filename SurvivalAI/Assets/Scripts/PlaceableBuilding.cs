﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    public BuildingTypes type;

    public bool isConstructed = false;
    public int health;
    public int maxHealth;

    public Material normal;
    public Material fade;

    public int attributedWood;
    public int attributedStone;

    public int neededWood;
    public int neededStone;

    [SerializeField] private TextMeshProUGUI attributedWoodText;
    [SerializeField] private TextMeshProUGUI attributedStoneText;

    [SerializeField] private TextMeshProUGUI neededWoodText;
    [SerializeField] private TextMeshProUGUI neededStoneText;

    [SerializeField] private Canvas constructUI;
    [SerializeField] private GameObject healthBar;

    private bool isSelected;

    public enum BuildingTypes
    {
        Forge,
        Culture,
        Mine,
        Entrepot,
        House,
        Tower
    }

    private void Start()
    {
        attributedWood = 0;
        attributedStone = 0;
        health = 0;

        switch (type)
        {
            case BuildingTypes.Forge:
                neededWood = 200;
                neededStone = 200;
                maxHealth = 200;
                break;
            case BuildingTypes.Culture:
                neededWood = 150;
                neededStone = 100;
                maxHealth = 200;
                break;
            case BuildingTypes.Mine:
                neededWood = 100;
                neededStone = 150;
                maxHealth = 200;
                break;
            case BuildingTypes.Entrepot:
                neededWood = 150;
                neededStone = 150;
                maxHealth = 200;
                break;
            case BuildingTypes.House:
                neededWood = 300;
                neededStone = 300;
                maxHealth = 500;
                break;
            case BuildingTypes.Tower:
                neededWood = 100;
                neededStone = 100;
                maxHealth = 200;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        checkAttributed();
        UpdateUI();
        UpdateHealthBar();

        if(isConstructed == true)
        {
            Destroy(constructUI);
        }
    }

    public bool checkAttributed()
    {
        if(neededWood == attributedWood && neededStone == attributedStone)
        {
            Debug.Log("Assez de ressources");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkConstructionStatus()
    {
        if (isConstructed == false && health == maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateUI()
    {
        attributedWoodText.text = attributedWood.ToString();
        attributedStoneText.text = attributedStone.ToString();
        neededWoodText.text = neededWood.ToString();
        neededStoneText.text = neededStone.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    public void DestroyConstruction()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Building")
        {
            colliders.Add(c);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Building")
        {
            colliders.Remove(c);
        }
    }

    public void SetSelected(bool s)
    {
        isSelected = s;
    }
}
