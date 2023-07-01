using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[HideInInspector]
    public Item ThisItem;

    private Image image;
    public TMP_Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int Count = 1;

    private void Awake()
    {
        image = GetComponent<Image>();
        countText = GetComponentInChildren<TMP_Text>();

        //RemoveImage();
    }

    public void InitialiseItem(Item newItem)
    {
        ThisItem = newItem;

        image.sprite = newItem.Image;

        RefreshCount();
        //SetUpImage(newItem.Image);
    }

    public void RefreshCount()
    {
        countText.text = Count.ToString();

        // the countText would only be needed if there is
        // more than one item of the same kind
        //countText.gameObject.SetActive(Count > 1);
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
