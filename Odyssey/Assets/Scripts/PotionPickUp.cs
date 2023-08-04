using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinding.RaycastModifier;

public class PotionPickUp : MonoBehaviour
{
    public Item Item;
    public bool Neared = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Item.Image;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Neared) return;
        Neared = true;

        ItemCollector collector = collision
            .GetComponent<ItemCollector>();

        if (!collector)
        {
            Neared = false;
            return;
        }

        if (collector.CollectPotions(Item))
        {
            Destroy(gameObject);
        }

        Neared = false;
    }
}
