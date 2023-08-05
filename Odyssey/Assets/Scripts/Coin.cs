using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float range = 0.7f;
    public int Quantity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemCollector coinCollector = collision
            .GetComponent<ItemCollector>();

        if (coinCollector)
        {
            coinCollector.CollectCoins(Quantity);
            Destroy(gameObject);
        }

    }

    public void SetQuantity(int MaxQuantity)
    {
        Quantity = (int)Random.Range(MaxQuantity * range, MaxQuantity);
    }
}
