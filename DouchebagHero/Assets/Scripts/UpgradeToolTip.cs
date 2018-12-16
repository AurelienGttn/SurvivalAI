using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeToolTip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI infos;
    [SerializeField] private TextMeshProUGUI necessaryWood;
    [SerializeField] private TextMeshProUGUI necessaryStone;

    private string infosText;
    private int neededWood;
    private int neededStone;

    private UpgradeManager upgradeManager;

    private Button button;

    private void Start()
    {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

        button = GetComponentInParent<Button>();
        if (button.name == "WeaponUp")
        {
            infos.text = upgradeManager.upgradeInfo[UpgradeManager.UpgradeTypes.Damage];
            necessaryWood.text = upgradeManager.necessaryWood[UpgradeManager.UpgradeTypes.Damage].ToString();
            necessaryStone.text = upgradeManager.necessaryStone[UpgradeManager.UpgradeTypes.Damage].ToString();
        }
        else if (button.name == "HarvestSpeedUp")
        {
            infos.text = upgradeManager.upgradeInfo[UpgradeManager.UpgradeTypes.GatherSpeed];
            necessaryWood.text = upgradeManager.necessaryWood[UpgradeManager.UpgradeTypes.GatherSpeed].ToString();
            necessaryStone.text = upgradeManager.necessaryStone[UpgradeManager.UpgradeTypes.GatherSpeed].ToString();
        }
        else if (button.name == "HarvestCarryUp")
        {
            infos.text = upgradeManager.upgradeInfo[UpgradeManager.UpgradeTypes.MaxBagStorage];
            necessaryWood.text = upgradeManager.necessaryWood[UpgradeManager.UpgradeTypes.MaxBagStorage].ToString();
            necessaryStone.text = upgradeManager.necessaryStone[UpgradeManager.UpgradeTypes.MaxBagStorage].ToString();
        }
    }

}

