using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBigCard : MonoBehaviour
{
    public ShopItemCard card;

    // Start is called before the first frame update
    void Awake()
    {
        ShopItemCard.OnClickItemCard += Open;
        card = GetComponent<ShopItemCard>();
        card.enabled = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(ShopItemCard shopItemCard)
    {
        gameObject.SetActive(true);
        card.item = shopItemCard.item;
        card.SetUp();
        card.enabled = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
