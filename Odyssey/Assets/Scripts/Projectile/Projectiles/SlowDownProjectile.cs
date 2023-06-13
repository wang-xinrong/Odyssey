using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownProjectile : Projectile
{
    public float SlowingFraction;
    public float Duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        PlayerController controller = collision.GetComponent<PlayerController>();

        if (controller != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            controller.SlowedDown(SlowingFraction, Duration);
            Destroy(gameObject);
        }
    }
}
