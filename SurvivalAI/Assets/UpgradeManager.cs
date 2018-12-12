using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public ResourceManager resourceManager;
    public PlayerAttack playerAttack;
    public WorkersManager workersManager;

    public enum UpgradeTypes
    {
        None,
        Damage,
        GatherSpeed,
        MaxBagStorage
    }

    public Dictionary<UpgradeTypes, string> upgradeInfo = new Dictionary<UpgradeTypes, string>();
    public Dictionary<UpgradeTypes, int> necessaryWood = new Dictionary<UpgradeTypes, int>();
    public Dictionary<UpgradeTypes, int> necessaryStone = new Dictionary<UpgradeTypes, int>();

    private void Awake()
    {
        upgradeInfo.Add(UpgradeTypes.Damage, "Increase Douchebag Hero's damage");
        upgradeInfo.Add(UpgradeTypes.GatherSpeed, "Increase peasants gathering speed");
        upgradeInfo.Add(UpgradeTypes.MaxBagStorage, "Increase peasants max carried resources");

        necessaryWood.Add(UpgradeTypes.Damage, 200);
        necessaryWood.Add(UpgradeTypes.GatherSpeed, 200);
        necessaryWood.Add(UpgradeTypes.MaxBagStorage, 200);

        necessaryStone.Add(UpgradeTypes.Damage, 200);
        necessaryStone.Add(UpgradeTypes.GatherSpeed, 200);
        necessaryStone.Add(UpgradeTypes.MaxBagStorage, 200);
    }

    public void upgradeDamage(int damageUp)
    {
        if(resourceManager.resourcesAvailable[ResourceTypes.Wood] >= necessaryWood[UpgradeTypes.Damage] && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= necessaryStone[UpgradeTypes.Damage])
        {
            playerAttack.damage += damageUp;
            resourceManager.RemoveResource(ResourceTypes.Wood, necessaryWood[UpgradeTypes.Damage]);
            resourceManager.RemoveResource(ResourceTypes.Stone, necessaryStone[UpgradeTypes.Damage]);
        }
    }

    public void upgradeGatheringSpeed(float gatheringSpeedUp)
    {
        if (resourceManager.resourcesAvailable[ResourceTypes.Wood] >= necessaryWood[UpgradeTypes.GatherSpeed] && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= necessaryStone[UpgradeTypes.GatherSpeed])
        {
            resourceManager.RemoveResource(ResourceTypes.Wood, necessaryWood[UpgradeTypes.GatherSpeed]);
            resourceManager.RemoveResource(ResourceTypes.Stone, necessaryStone[UpgradeTypes.GatherSpeed]);
            foreach (WorkerBT worker in workersManager.workers)
            {
                worker.gatheringSpeed += gatheringSpeedUp;
            }
        }

    }

    public void upgradeGatheringCarry(int gatheringCarryUp)
    {
        if (resourceManager.resourcesAvailable[ResourceTypes.Wood] >= necessaryWood[UpgradeTypes.MaxBagStorage] && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= necessaryStone[UpgradeTypes.MaxBagStorage])
        {
            resourceManager.RemoveResource(ResourceTypes.Wood, necessaryWood[UpgradeTypes.MaxBagStorage]);
            resourceManager.RemoveResource(ResourceTypes.Stone, necessaryStone[UpgradeTypes.MaxBagStorage]);
            foreach (WorkerBT worker in workersManager.workers)
            {
                worker.maxResources += gatheringCarryUp;
            }
        }
    }
}
