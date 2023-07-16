using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemCard : MonoBehaviour
{
    public Item item;
    public Image Image;
    public TMP_Text DescriptionText;
    public TMP_Text QuantityText;
    private int quantity = 1;
    public int MaxPurchaseQuantity = 99;
    public int MinPurchaseQuantity = 1;
    public TMP_Text NameText;

    //number texts
    public TMP_Text HPText;
    public TMP_Text SPText;
    public TMP_Text SpeedText;
    public TMP_Text DamageText;
    public TMP_Text DurationText;
    public TMP_Text PriceText;

    //for disabling
    public GameObject Buttons;
    public GameObject Texts;
    public GameObject Icon;

    //for "enlargement" when clicked
    public delegate void ClickItemCard(ShopItemCard shopItemCard);
    public static event ClickItemCard OnClickItemCard;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void setEveryUIInactive()
    {
        Buttons.SetActive(false);
        Texts.SetActive(false);
        Icon.SetActive(false);
    }

    private void OnEnable()
    {
        quantity = MinPurchaseQuantity;
        RefreshQuantityText();
    }

    public void QuantityAdd()
    {
        // max quantity allowed
        if (quantity == MaxPurchaseQuantity) return;

        quantity++;
        RefreshQuantityText();
    }

    public void QuantityMinus()
    {
        // minimum quantity allowed
        if (quantity == MinPurchaseQuantity) return;

        quantity--;
        RefreshQuantityText();
    }

    public void RefreshQuantityText()
    {
        QuantityText.text = quantity.ToString();
    }

    private void SetUpText()
    {
        NameText.text = item.NameString;
        RefreshQuantityText();
        HPText.text = item.HealthIncrease.ToString();
        SPText.text = item.SPIncrease.ToString();
        SpeedText.text = item.MovementIncrease.ToString();
        DamageText.text = item.DamageIncrease.ToString();
        DurationText.text = item.Duration.ToString();
        PriceText.text = item.price.ToString();
        DescriptionText.text = item.Description;
    }

    public void Purchase()
    {
        InventoryManager.Instance.Purchase(item, quantity);
    }

    public void OnEnlarge()
    {
        OnClickItemCard.Invoke(this);
    }

    public void SetUp()
    {
        if (!item)
        {
            setEveryUIInactive();
            return;
        }

        Image.sprite = item.Image;
        SetUpText();
    }
}
