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
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

        if (button.name == "Forge")
        {
            infos.text = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Forge];
            necessaryWood.text = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Forge].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Forge].ToString();
        }
        else if (button.name == "Culture")
        {
            infos.text = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Culture];
            necessaryWood.text = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Culture].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Culture].ToString();
        }
        else if (button.name == "Entrepot")
        {
            infos.text = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Entrepot];
            necessaryWood.text = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Entrepot].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Entrepot].ToString();
        }
        else if (button.name == "Tower")
        {
            infos.text = buildingManager.buildingInfo[BuildingManager.BuildingTypes.Tower];
            necessaryWood.text = buildingManager.necessaryWood[BuildingManager.BuildingTypes.Tower].ToString();
            necessaryStone.text = buildingManager.necessaryStone[BuildingManager.BuildingTypes.Tower].ToString();
        }
    }
}
