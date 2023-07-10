using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int MaxQuantity;
    public float range = 0.9f;
    public int Quantity;

    // Start is called before the first frame update
    void Start()
    {
        Quantity = (int) Random.Range(MaxQuantity * range, MaxQuantity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollector coinCollector = collision
            .GetComponent<CoinCollector>();

        if (coinCollector)
        {
            coinCollector.CollectCoins(Quantity);
            Destroy(gameObject);
        }

    }
}
