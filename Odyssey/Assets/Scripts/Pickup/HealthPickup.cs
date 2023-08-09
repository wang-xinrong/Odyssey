using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int HealingAmount = 20;
    public enum Type { Solid, Liquid}
    public Type ItemType;
    private bool hasHealed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHealed)
        {
            return;
        }
        Damageable _damageable = collision.GetComponent<Damageable>();

        if (_damageable)
        {
            // if a healed successfully
            if (_damageable.OnHeal(HealingAmount))
            {
                if (ItemType == Type.Solid) ActionSoundEffects.PlayFoodConsumption();
                if (ItemType == Type.Liquid) ActionSoundEffects.PlayLiquidFoodConsumption();
                hasHealed = true;
                Destroy(gameObject);
            }
        }
    }
}
