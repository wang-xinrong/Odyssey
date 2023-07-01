using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject DraggableItemPrefab;

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
            }
        }
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(DraggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItemGO.GetComponent<DraggableItem>();
        draggableItem.InitialiseItem(item);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
