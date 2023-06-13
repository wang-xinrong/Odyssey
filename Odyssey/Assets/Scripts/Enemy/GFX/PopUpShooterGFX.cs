using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpShooterGFX : EnemyGFX
{
    void Update()
    {
        // the sprite needs not be flipped if the enemy is dead
        if (_currentState != State.Death) FlipSpriteToFacePlayer();
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.EnemyDeath);
        }
        else if (_currentState == State.Attack)
        {
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.EnemyAttack);
        }
        else if (_currentState == State.Idle)
        {
            PlayAnimation(AnimationNames.EnemyIdle);
        }
    }
}