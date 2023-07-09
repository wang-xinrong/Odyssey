using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShooterGFX : EnemyGFX
{
    void Update()
    {
        // the sprite needs not be flipped if the enemy is dead
        if (_currentState != State.Death) FlipSpriteToFacePlayer();

        _attackTimer += Time.deltaTime;

        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.OldVillagerWomanDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.OldVillagerWomanAttack);
        }
        else if (_currentState == State.Idle)
        {
            PlayAnimation(AnimationNames.OldVillagerWomanIdle);
        }
    }
}