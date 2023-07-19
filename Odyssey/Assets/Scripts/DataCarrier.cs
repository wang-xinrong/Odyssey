using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public static DataCarrier Instance;
    //public InventoryData InventoryData;
    public Item[] Items;
    public int[] Quantities;
    private InventoryManager inventoryManager;
    public int InitialMoney;
    public int LastUpdatedMoney;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateInventoryData()
    {
        inventoryManager = GameObject.Find("InventoryManager")
            .GetComponent<InventoryManager>();

        Items = new Item[inventoryManager
            .InventorySlots.Length];

        Quantities = new int[inventoryManager
            .InventorySlots.Length];

        for (int i = 0; i < inventoryManager.InventorySlots.Length; i++)
        {
            Items[i] = inventoryManager.InventorySlots[i]
                .GetItemStored();

            Quantities[i] = inventoryManager.InventorySlots[i]
                .GetQuantityStored();
        }

        LastUpdatedMoney = inventoryManager.Money;
    }

    public void WriteInventoryData()
    {
        
        inventoryManager = GameObject.Find("InventoryManager")
            .GetComponent<InventoryManager>();

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i])
            {
                inventoryManager.OverwriteSlotItem(Items[i]
                    , Quantities[i]
                    , i);
            }
        }

        inventoryManager.Money = LastUpdatedMoney;
    }

    public void ClearInventoryData()
    {
        inventoryManager = GameObject.Find("InventoryManager")
            .GetComponent<InventoryManager>();

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i])
            {
                inventoryManager.OverwriteSlotItem(null
                    , 1
                    , i);
            }
        }

        inventoryManager.Money = InitialMoney;
        LastUpdatedMoney = InitialMoney;
    }
}
