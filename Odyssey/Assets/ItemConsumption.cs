using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumption : MonoBehaviour
{
    public MainPlayerController MainPlayerController;
    private PlayerController activeCharController;

    public bool ConsumeItem(Item i)
    {
        activeCharController = MainPlayerController.GetComponentInChildren<PlayerController>();

        // both chars are dead
        if (!activeCharController) return false;

        bool spReplenished = MainPlayerController.ReplenishSP(i.SPIncrease);
        bool hpReplenished = activeCharController.ReplenishHealth(i.HealthIncrease);
        bool movementSpeedUp = activeCharController.SpeedUp(i.MovementIncrease, i.Duration);

        // the item can be used if any of its functions can take effect
        // in this case since the SpeedUp will always work, the item can
        // always be used
        return spReplenished || hpReplenished || movementSpeedUp;
    }
}
