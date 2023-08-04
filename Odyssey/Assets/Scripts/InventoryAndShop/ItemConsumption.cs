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
        bool result = false;


        // both chars are dead
        if (!activeCharController) return false;

        if (i.ItemType == Item.Type.SP)
        {
            result = MainPlayerController.ReplenishSP(i.SPIncrease);
            //return MainPlayerController.ReplenishSP(i.SPIncrease);
            if (!result) CharacterEvents.GenerateFeedbackAtBottom("failed sp potion usage - full sp");
        }
        if (i.ItemType == Item.Type.HP)
        {
            result = activeCharController.ReplenishHealth(i.HealthIncrease);
            //return activeCharController.ReplenishHealth(i.HealthIncrease);
            if (!result) CharacterEvents.GenerateFeedbackAtBottom("failed hp potion usage - full hp");
        }
        if (i.ItemType == Item.Type.Speed)
        {
            result = activeCharController.SpeedUp(i.MovementIncrease, i.Duration);
            if (!result) CharacterEvents.GenerateFeedbackAtBottom("failed speed potion usage - max speed");
        }

        return result;
    }
}
