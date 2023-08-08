using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftPromptTrigger : KeyPromptTrigger
{
    public Dashed player;
    protected override bool OnCondition()
    {
        return player.HasDashed;
    }

    protected override bool OnTerminateCondition()
    {
        return OnCondition();
    }
}
