using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownProjectile : Projectile
{
    public float SlowingFraction;
    public float Duration;
    

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (ProjectileTargetType == TargetType.Player)
        {
            AgainstPlayer(collision);
        }

        if (ProjectileTargetType == TargetType.Enemy)
        {
            AgainstEnemy(collision);
        }
    }

    private void AgainstPlayer(Collider2D collision)
    {
        // see if it can be hit
        PlayerController controller = collision.GetComponent<PlayerController>();
        Damageable damageable = collision.GetComponent<Damageable>();

        if (controller != null)
        {
            controller.SlowedDown(SlowingFraction, Duration);
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }
    }

    private void AgainstEnemy(Collider2D collision)
    {
        // see if it can be hit
        EnemyMovement EM = collision.GetComponent<EnemyMovement>();
        AISpecialEffect AIS = collision.GetComponent<AISpecialEffect>();
        Damageable damageable = collision.GetComponent<Damageable>();

        // check which type of movement control the
        // enemy is using
        if (EM != null && AIS == null)
        {
            EM.SlowedDown(SlowingFraction, Duration);
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }

        if (EM == null && AIS != null)
        {
            AIS.SlowedDown(SlowingFraction, Duration);
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }
    }
}