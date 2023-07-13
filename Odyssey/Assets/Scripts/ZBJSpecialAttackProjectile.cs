using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZBJSpecialAttackProjectile : Projectile
{
    private new void Start()
    {
        base.Start();
        Damage = StatsManager.Instance.GetCharacterSA(
            StatsManager.Character.Pigsy);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }
    }
}
