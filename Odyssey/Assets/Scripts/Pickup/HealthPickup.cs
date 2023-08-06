using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int HealingAmount = 20;
    public enum Type { Solid, Liquid}
    public Type ItemType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable _damageable = collision.GetComponent<Damageable>();

        if (_damageable)
        {
            // if a healed successfully
            if (_damageable.OnHeal(HealingAmount))
            {
                if (ItemType == Type.Solid) ActionSoundEffects.PlayFoodConsumption();
                Destroy(gameObject);
            }
        }
    }
}
