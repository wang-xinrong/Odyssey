using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[HideInInspector]
    public Item ThisItem;

    private Image image;
    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        image = GetComponent<Image>();
        //RemoveImage();
        InitialiseItem(ThisItem);
    }

    public void InitialiseItem(Item newItem)
    {
        ThisItem = newItem;

        image.sprite = newItem.Image;

        //SetUpImage(newItem.Image);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        // to make sure the image is always rendered on top of
        // everything else
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    /*
    private void SetUpImage(Sprite s)
    {
        image.sprite = s;
        image.enabled = true;
    }

    private void RemoveImage()
    {
        image.sprite = null;
        image.enabled = false;
    }
    */
}
