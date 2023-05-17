using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int AttackDamage = 10;
    public Vector2 KnockBack = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // hit the target
            damageable.Hurt(AttackDamage, KnockBack);
        }
    }
}
