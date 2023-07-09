using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject DraggableItemPrefab;
    public int MaxItemCount = 5;
    //public ItemConsumption ItemConsumption;

    public static InventoryManager Instance;
    public ItemConsumption ItemConsumption;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // returns if item is successfully added to an available space
    public bool AddItem(Item item)
    {
        InventorySlot slot;
        DraggableItem existingItem;

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            slot = InventorySlots[i];
            existingItem = slot.GetComponentInChildren<DraggableItem>();

            // if there is not already an item
            if (!existingItem)
            {
                // spawn the item in the first available slot
                // from top-down, left-right order
                SpawnNewItem(item, slot);
                //return;
                return true;
            }

            if (existingItem.ThisItem == item
                && existingItem.Count < MaxItemCount
                && item.Stackable)
            {
                existingItem.Count++;
                existingItem.RefreshCount();
                return true;
            }
            
        }

        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(DraggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItemGO.GetComponent<DraggableItem>();
        draggableItem.InitialiseItem(item);
    }

    public int NumSlotsInUsageRow = 6;

    public void UseItem(int slotIndex)
    {
        if (GameStatus.Instance.IsGamePaused) return;

        // the slotIndex should be smaller or equal to
        // # usage row slots - 1
        if (slotIndex >= NumSlotsInUsageRow)
        {
            Debug.Log("UseItem index exceeds # UsageRow slots");
            return;
        }

        InventorySlot slot;
        DraggableItem item;

        slot = InventorySlots[slotIndex];
        item = slot.GetComponentInChildren<DraggableItem>();

        // if there is no item
        if (!item)
        {
            Debug.Log("no item in the called usage row slot");
            return;
        }

        ConsumeItem(item);
    }

    private void ConsumeItem(DraggableItem item)
    {
        bool success = ItemConsumption.ConsumeItem(item.ThisItem);

        // failed to use the item
        if (!success) return;

        item.Consumed();
    }

    public void OnUseItem1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(0);
        }
    }

    public void OnUseItem2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(1);
        }
    }

    public void OnUseItem3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(2);
        }
    }

    public void OnUseItem4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(3);
        }
    }

    public void OnUseItem5(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(4);
        }
    }

    public void OnUseItem6(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseItem(5);
        }
    }

    // wallet
    public int Money = 0;

    public bool Purchase(Item item, int Quantity)
    {
        int expense = item.price * Quantity;
        if (expense > Money) return false;

        for (int i = 0; i < Quantity; i++)
        {
            if (!AddItem(item))
            {
                Debug.Log("Only " +
                    i + " items purchased due to " +
                    "limited inventory space");
                Money -= i * item.price;
                return false;
            }
        }
        Money -= expense;
        return true;
    }
}
