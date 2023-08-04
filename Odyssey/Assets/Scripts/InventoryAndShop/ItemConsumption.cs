using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumption : MonoBehaviour
{
    public MainPlayerController MainPlayerController;
    private PlayerController activeCharController;

    public void Awake()
    {
        MainPlayerController = GetComponent<MainPlayerController>();
    }

    public bool ConsumeItem(Item i)
    {
        activeCharController = MainPlayerController.GetComponentInChildren<PlayerController>();

        // both chars are dead
        if (!activeCharController) return false;

        if (i.ItemType == Item.Type.SP) return MainPlayerController.ReplenishSP(i.SPIncrease);
        if (i.ItemType == Item.Type.HP)
        {
            bool result = activeCharController.ReplenishHealth(i.HealthIncrease);
            //return activeCharController.ReplenishHealth(i.HealthIncrease);
            if (!result) CharacterEvents.GenerateFeedbackAtBottom("failed hp potion usage - full hp");
            return result;
        }
        if (i.ItemType == Item.Type.Speed) return activeCharController.SpeedUp(i.MovementIncrease, i.Duration);

        return false;
    }
}
