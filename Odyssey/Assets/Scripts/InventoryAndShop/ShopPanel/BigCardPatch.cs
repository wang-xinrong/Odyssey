using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCardPatch : MonoBehaviour
{
    public ShopBigCard bigCard;

    public void OnEnlarge(ShopItemCard card)
    {
        bigCard.Open(card);
    }
}
