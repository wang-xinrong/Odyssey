using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject DraggableItemPrefab;
    public int MaxItemCount = 5;
    //public ItemConsumption ItemConsumption;

    // returns if item is successfully added to an available space
    public void AddItem(Item item)
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
                return;
                //return true;
            }

            if (existingItem.ThisItem == item
                && existingItem.Count < MaxItemCount
                && item.Stackable)
            {
                existingItem.Count++;
                existingItem.RefreshCount();
                return;
            }
            
        }

        //return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(DraggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItemGO.GetComponent<DraggableItem>();
        draggableItem.InitialiseItem(item);
    }

    public void UseItem(int slotIndex)
    {
        // the slotIndex should be smaller or equal to
        // # usage row slots - 1

        InventorySlot slot;
        DraggableItem item;

        slot = InventorySlots[slotIndex];
        item = slot.GetComponentInChildren<DraggableItem>();

        // if there is no item
        if (!item)
        {
            Debug.Log("no item in the called usage row slot");
        }

        //ItemConsumption.ConsumeItem(item.ThisItem);
    }
}
