using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBarPromptTrigger : KeyPromptTrigger
{
    // Start is called before the first frame update
    public MainPlayerController MainPlayerController;
    public Damageable EnemyDamageable;

    private void Start()
    {
        MainPlayerController = GameObject.Find("Player")
            .GetComponent<MainPlayerController>();
    }

    protected override bool OnCondition()
    {
        return !MainPlayerController.isChar1;
    }

    protected override bool OnTerminateCondition()
    {
        return !EnemyDamageable.IsAlive;
    }
}
