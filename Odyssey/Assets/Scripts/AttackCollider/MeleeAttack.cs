using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int AttackDamage = 10;
    public Vector2 KnockBack = Vector2.zero;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();
        damageable.OnHurt(AttackDamage, Vector2.zero);
        /*
        if (damageable != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            damageable.OnHurt(AttackDamage
                           , new Vector2(KnockBack.x * gameObject.transform
                                                           .parent.transform
                                                           .localScale.x
                                 , KnockBack.y * gameObject
                                     .transform.parent
                                     .transform.localScale.y));
        }
        */
    }
}
