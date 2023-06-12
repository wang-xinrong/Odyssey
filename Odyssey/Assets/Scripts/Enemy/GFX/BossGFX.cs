using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGFX : EnemyGFX
{
    private BossStageManager _bossStageManager;

    private new void Start()
    {
        base.Start();
        _bossStageManager = GetComponent<BossStageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bossStageManager._currentBossStage == BossStageManager.BossStage.One)
        {
            StageOne();
        }

        if (_bossStageManager._currentBossStage == BossStageManager.BossStage.Two)
        {
            StageTwo();
        }

        if (_bossStageManager._currentBossStage == BossStageManager.BossStage.Three)
        {
            StageThree();
        }
    }

    void StageOne()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.StageOneDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.StageOneAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.StageOneWalk);
        }
        else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.StageOneIdle);
        }
    }

    void StageTwo()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.StageTwoDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.StageTwoAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.StageTwoWalk);
        }
        else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.StageTwoIdle);
        }
    }

    // used to call EnlargeScale once
    private bool hasEnlarged = false;

    void StageThree()
    {
        if (!hasEnlarged)
        {
            transform.localScale = Scaling(transform.localScale, 2);
            hasEnlarged = true;
        }

        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.StageThreeDeath);
        }
        else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.StageThreeAttack);
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.StageThreeWalk);
        }
        else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.StageThreeIdle);
        }
    }
}
