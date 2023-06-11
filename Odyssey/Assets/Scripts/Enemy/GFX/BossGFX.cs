using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGFX : EnemyGFX
{
    private enum BossStage { One, Two, Three }
    private BossStage _currentBossStage = BossStage.One;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StageOne()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.EnemyDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.EnemyAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.EnemyWalk);
        }
        else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.EnemyIdle);
        }
    }

    void StageTwo()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.EnemyDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.EnemyAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.EnemyWalk);
        }
        else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.EnemyIdle);
        }
    }

    void StageThree()
    {
        void Update()
        {
            NonAIDetermineDirectionAndFlipSprite();
            _attackTimer += Time.deltaTime;
            if (_damageable.IsAlive == false)
            {
                _currentState = State.Death;
                PlayAnimation(AnimationNames.EnemyDeath);
            }
            else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
            {
                _currentState = State.Attack;
                // pass the responsibility to the launcher to decide direction
                TargetTransform = AttackZone.TargetTransform;
                PlayAnimation(AnimationNames.EnemyAttack);
                Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (_rb.velocity.magnitude > 1f)
            {
                _currentState = State.Walk;
                PlayAnimation(AnimationNames.EnemyWalk);
            }
            else if (_rb.velocity.magnitude < 1f || _currentState == State.Idle)
            {
                _currentState = State.Idle;
                PlayAnimation(AnimationNames.EnemyIdle);
            }
        }
    }
}
