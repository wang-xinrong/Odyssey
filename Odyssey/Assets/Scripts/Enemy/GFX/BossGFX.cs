using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGFX : EnemyGFX
{
    private BossStageManager _bossStageManager;
    public Transform TopLeftRoomCorner;
    public Transform BottomRightRoomCorner;
    public float SummoningInterval;
    private float _summoningTimer = 0f;

    public GameObject StageOneSummonedMinionPrefab;
    public GameObject StageTwoSummonedMinionPrefab;

    private new void Start()
    {
        base.Start();
        _bossStageManager = GetComponent<BossStageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bossStageManager._currentBossStage == BossStageManager.BossStage.Zero)
        {
            StageZero();
        }

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

    void StageZero()
    {
        TransitionBetweenStageZeroAndOne();
    }

    void StageOne()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        _summoningTimer += Time.deltaTime;

        TimedSummoning(StageOneSummonedMinionPrefab, 1, SummoningInterval);

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
        _summoningTimer += Time.deltaTime;

        TimedSummoning(StageTwoSummonedMinionPrefab, 2, SummoningInterval);

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
        /*
        if (!hasEnlarged)
        {
            transform.localScale = Scaling(transform.localScale, 2);
            hasEnlarged = true;
        }
        */

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

    public void TransitionBetweenStageZeroAndOne()
    {
        Directions.SetPositionToCentreOfVectorInputs(gameObject
            , TopLeftRoomCorner, BottomRightRoomCorner);
        PlayAnimation(AnimationNames.StageOneEntry);
    }

    public void TransitionBetweenStageOneAndTwo()
    {
        PlayAnimation(AnimationNames.StageTwoEntry);
        Directions.SetPositionToCentreOfVectorInputs(gameObject
            , TopLeftRoomCorner, BottomRightRoomCorner);
    }

    public void TransitionBetweenStageTwoAndThree()
    {
        PlayAnimation(AnimationNames.StageThreeEntry);
        Directions.SetPositionToCentreOfVectorInputs(gameObject
            , TopLeftRoomCorner, BottomRightRoomCorner);

        if (!hasEnlarged)
        {
            transform.localScale = Scaling(transform.localScale, 2);
            hasEnlarged = true;
        }
    }

    public void TimedSummoning(GameObject minion, int numberOfMinionsToSummon, float interval)
    {
        if (_summoningTimer < interval) return;

        Summon(minion, numberOfMinionsToSummon);
        _summoningTimer = 0f;
    }

    public void Summon(GameObject minion, int numberOfMinionsToSummon)
    {
        for (int i = 0; i < numberOfMinionsToSummon; i++)
        {

            Vector2 targetPosition = Directions
                .RandomisePosition(TopLeftRoomCorner, BottomRightRoomCorner);

            Instantiate(minion, targetPosition, Quaternion.identity);
        }
    }

}
