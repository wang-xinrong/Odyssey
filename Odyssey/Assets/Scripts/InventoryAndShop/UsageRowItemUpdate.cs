using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsageRowItemUpdate : MonoBehaviour
{
    public InventorySlot InventorySlot;
    public TMP_Text CountText;
    public Image Image;
    private Sprite sprite;
    private int count = 0;
    private DraggableItem draggableItem;

    private void Start()
    {
        CountText.enabled = count > 1;
    }

    // Update is called once per frame
    void Update()
    {
        draggableItem = InventorySlot.GetComponentInChildren<DraggableItem>();

        if (!draggableItem)
        {
            Image.enabled = false;
            CountText.enabled = false;
            return;
        }

        SetUpSpriteAndCountText();

        Image.enabled = true;
        CountText.enabled = count > 1;
    }

    private void SetUpSpriteAndCountText()
    {
        sprite = draggableItem.ThisItem.Image;
        count = InventorySlot.GetComponentInChildren<DraggableItem>().Count;
        Image.sprite = sprite;
        CountText.text = count.ToString();
    }
}
