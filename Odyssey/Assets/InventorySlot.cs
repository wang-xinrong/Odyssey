using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();

        // there is already an item under the inventory slot
        if (transform.childCount <= 0)
        {
            draggableItem.parentAfterDrag = transform;
            return;
        }

        // transform.childCount > 0
        DraggableItem existingItem = gameObject.transform.GetChild(0)
            .gameObject.GetComponent<DraggableItem>();

        // different item type
        if (!existingItem.ShareTheSameItemType(draggableItem))
        {
            return;
        }

        // same item type

        int totalCount = draggableItem.Count + existingItem.Count;

        if (totalCount <= InventoryManager.Instance.MaxItemCount)
        {
            existingItem.Count = totalCount;
            existingItem.RefreshCount();
            Destroy(droppedItem);
            return;
        }

        if (totalCount > InventoryManager.Instance.MaxItemCount)
        {
            existingItem.Count = InventoryManager.Instance.MaxItemCount;
            existingItem.RefreshCount();
            draggableItem.Count = totalCount -
                InventoryManager.Instance.MaxItemCount;
            draggableItem.RefreshCount();
            return;
        }
    }
}
