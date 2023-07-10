using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
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
}
