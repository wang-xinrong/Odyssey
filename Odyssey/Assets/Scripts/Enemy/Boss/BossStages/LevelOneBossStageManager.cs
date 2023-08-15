using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwapMovementBehaviour))]
public class LevelOneBossStageManager : BossStageManager
{
    private SwapMovementBehaviour _swapMovementBehaviour;
    private BossStage OldBossStage = BossStage.Zero;

    private new void Start()
    {
        base.Start();
        _swapMovementBehaviour = GetComponent<SwapMovementBehaviour>();
    }

    void Update()
    {
        SetStageBehaviour(_currentBossStage);
    }

    private void SetStageBehaviour(BossStage stage)
    {
        if (stage == OldBossStage) return;

        // define the boss behaviour for various stages here
        if (stage == BossStage.One)
        {
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Patrol);
            _swapMovementBehaviour.ChangePatrolTargetSets(0);
            OldBossStage = BossStage.One;
            return;
        }

        if (stage == BossStage.Two)
        {
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Patrol);
            _swapMovementBehaviour.ChangePatrolTargetSets(1);
            OldBossStage = BossStage.Two;
            return;
        }

        if (stage == BossStage.Three)
        {
            _swapMovementBehaviour.Swap(SwapMovementBehaviour.CurrentState.Chasing);
            OldBossStage = BossStage.Three;
            return;
        }
    }
}

