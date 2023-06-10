using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeAttackerGFX : EnemyGFX
{
    void Update()
    {
        NonAIDetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.RedHairWomanDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.RedHairWomanAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_rb.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.RedHairWomanWalk);
        }
        else if (_rb.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.RedHairWomanIdle);
        }
    }
}
