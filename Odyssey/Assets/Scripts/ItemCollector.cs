using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager")
            .GetComponent<InventoryManager>();
    }

    public void CollectCoins(int amount)
    {
        inventoryManager.Money += amount;
    }

    // returns whether the inventory successfully added the item
    public bool CollectPotions(Item item)
    {
        bool result = inventoryManager.AddItem(item);
        if (!result) CharacterEvents.GenerateFeedbackAtBottom(
            "potion pickup failed - full inventory");

        return result;
    }
}
