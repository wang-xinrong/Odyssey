using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMBPromptTrigger : KeyPromptTrigger
{
    public Damageable EnemyDamageable;

    protected override bool OnCondition()
    {
        return !EnemyDamageable.IsAlive;
    }

    protected override bool OnTerminateCondition()
    {
        return OnCondition();
    }
}
