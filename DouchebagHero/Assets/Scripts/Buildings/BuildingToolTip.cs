using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BuildingToolTip : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI infos;
    [SerializeField] private TextMeshProUGUI necessaryWood;
    [SerializeField] private TextMeshProUGUI necessaryStone;

    private string infosText;
    private int neededWood;
    private int neededStone;

    public BuildingManager buildingManager;

    private Button button;

    private void Start()
    {
        button = GetComponentInParent<Button>();
        buildingManager = FindObjectOfType<BuildingManager>();

        if (button.name == "Forge")
        {
            infos.text = buildingManager.buildingInfo[BuildingTypes.Forge];
            necessaryWood.text = buildingManager.necessaryWood[BuildingTypes.Forge].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingTypes.Forge].ToString();
        }
        else if (button.name == "Culture")
        {
            infos.text = buildingManager.buildingInfo[BuildingTypes.Culture];
            necessaryWood.text = buildingManager.necessaryWood[BuildingTypes.Culture].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingTypes.Culture].ToString();
        }
        else if (button.name == "Warehouse")
        {
            infos.text = buildingManager.buildingInfo[BuildingTypes.Warehouse];
            necessaryWood.text = buildingManager.necessaryWood[BuildingTypes.Warehouse].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingTypes.Warehouse].ToString();
        }
        else if (button.name == "Tower")
        {
            infos.text = buildingManager.buildingInfo[BuildingTypes.Tower];
            necessaryWood.text = buildingManager.necessaryWood[BuildingTypes.Tower].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingTypes.Tower].ToString();
        }
    }
}
