using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPromptTrigger : KeyPromptTrigger
{
    protected override bool OnCondition()
    {
        return InventoryManager.Instance.AnyItemInUsageRow();
    }

    protected override bool OnTerminateCondition()
    {
        return OnCondition();
    }

    private void Update()
    {
        if (OnCondition()) gameObject.SetActive(false);
    }
}
