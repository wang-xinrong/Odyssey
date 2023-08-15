using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZBJSpecialAttackProjectile : Projectile
{
    public float SlowingFraction;
    public float Duration;

    private new void Start()
    {
        base.Start();
        Damage = StatsManager.Instance.GetCharacterSA(
            StatsManager.Character.Pigsy);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        AgainstEnemy(collision);
    }

    // function copied from SlowDownProjectile
    private void AgainstEnemy(Collider2D collision)
    {
        if (collision.tag == "Door" || collision.tag == "Wall") return;

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
            return;
        }

        if (EM == null && AIS != null)
        {
            AIS.SlowedDown(SlowingFraction, Duration);
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
            return;
        }

        if (damageable)
        {
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
            return;
        }
    }
}
