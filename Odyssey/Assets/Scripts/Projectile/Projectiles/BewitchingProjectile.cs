using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BewitchingProjectile : Projectile
{
    public float Duration;

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // see if it can be hit
        PlayerController controller = collision.GetComponent<PlayerController>();
        Damageable damageable = collision.GetComponent<Damageable>();

        if (controller != null && damageable != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            controller.Bewitched(Duration);
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }
    }
}

