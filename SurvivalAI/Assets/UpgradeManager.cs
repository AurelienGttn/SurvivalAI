using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public ResourceManager resourceManager;
    public PlayerAttack playerAttack;
    public WorkersManager workersManager;

    public void upgradeDamage(int damageUp)
    {
        if(resourceManager.resourcesAvailable[ResourceTypes.Wood] >= 200 && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= 300)
        {
            playerAttack.damage += damageUp;
            resourceManager.RemoveResource(ResourceTypes.Wood, 200);
            resourceManager.RemoveResource(ResourceTypes.Stone, 300);
        }
    }

    public void upgradeGatheringSpeed(float gatheringSpeedUp)
    {
        if (resourceManager.resourcesAvailable[ResourceTypes.Wood] >= 300 && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= 300)
        {
            resourceManager.RemoveResource(ResourceTypes.Wood, 300);
            resourceManager.RemoveResource(ResourceTypes.Stone, 300);
            foreach (WorkerBT worker in workersManager.workers)
            {
                worker.gatheringSpeed += gatheringSpeedUp;
            }
        }

    }

    public void upgradeGatheringCarry(int gatheringCarryUp)
    {
        if (resourceManager.resourcesAvailable[ResourceTypes.Wood] >= 300 && resourceManager.resourcesAvailable[ResourceTypes.Stone] >= 300)
        {
            resourceManager.RemoveResource(ResourceTypes.Wood, 300);
            resourceManager.RemoveResource(ResourceTypes.Stone, 300);
            foreach (WorkerBT worker in workersManager.workers)
            {
                worker.maxResources += gatheringCarryUp;
            }
        }
    }
}
