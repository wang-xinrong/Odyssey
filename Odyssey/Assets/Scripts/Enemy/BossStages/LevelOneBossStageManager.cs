using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwapMovementBehaviour))]
public class LevelOneBossStageManager : BossStageManager
{
    private SwapMovementBehaviour _swapMovementBehaviour;

    private new void Start()
    {
        base.Start();
        _swapMovementBehaviour = GetComponent<SwapMovementBehaviour>();
    }

    void Update()
    {
        if (_damageable.Health >= _damageable.MaxHealth * 2 / 3)
        {
            _currentBossStage = BossStage.One;
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Patrol);
            _swapMovementBehaviour.ChangePatrolTargetSets(0);
        }
        else if (_damageable.Health >= _damageable.MaxHealth * 1 / 3)
        {
            _currentBossStage = BossStage.Two;
            // maybe bring the boss to a certain position
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Patrol);
            _swapMovementBehaviour.ChangePatrolTargetSets(1);
        }
        else
        {
            _currentBossStage = BossStage.Three;
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Chasing);
        }
    }
}

