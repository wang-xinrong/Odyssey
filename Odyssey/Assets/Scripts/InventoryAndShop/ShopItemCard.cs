using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemCard : MonoBehaviour
{
    public bool IsBigCard = false;

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
        DescriptionText.text = item.Description;
        PriceText.text = item.price.ToString();
        if (!IsBigCard) return;

        HPText.text = item.HealthIncrease.ToString();
        SPText.text = item.SPIncrease.ToString();
        SpeedText.text = item.MovementIncrease.ToString();
        DamageText.text = item.DamageIncrease.ToString();
        DurationText.text = item.Duration.ToString();
    }

    public void Purchase()
    {
        bool result = InventoryManager.Instance.Purchase(item, quantity);
        if (!result) CharacterEvents.GenerateFeedback.Invoke("insufficent coin");
        if (result) UIInteractionSoundEffect.PlayPurchaseSound();
    }

    public void OnEnlarge()
    {
        //Debug.Log(GameObject.Find("ShopPanel").GetComponent<BigCardPatch>());
        GameObject.Find("ShopPanel").GetComponent<BigCardPatch>().OnEnlarge(this);
        //OnClickItemCard.Invoke(this);
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
