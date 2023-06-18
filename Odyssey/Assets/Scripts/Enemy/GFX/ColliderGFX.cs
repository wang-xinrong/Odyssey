using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGFX : EnemyGFX
{
    void Update()
    {
        //NonAIDetermineDirectionAndFlipSprite();
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.DancingGirlDeath);
        } else if (_rb.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.DancingGirlSliding);
        }
    }
}
