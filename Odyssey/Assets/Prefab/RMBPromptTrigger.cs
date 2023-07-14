using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMBPromptTrigger : KeyPromptTrigger
{
    // Start is called before the first frame update
    public MainPlayerController MainPlayerController;
    public PlayerController PlayerController;
    public Damageable EnemyDamageable;

    private void Start()
    {
        MainPlayerController = GameObject.Find("Player")
            .GetComponent<MainPlayerController>();
    }

    protected override bool OnCondition()
    {
        PlayerController = MainPlayerController
            .GetCurrentCharacterPlayerController();

        return MainPlayerController.SP < PlayerController
            ._specialAttack.specialAttackCost;
    }

    protected override bool OnTerminateCondition()
    {
        return !EnemyDamageable.IsAlive;
    }

    protected new void OnTriggerStay2D(Collider2D collision)
    {
        if (!OnCondition())
        {
            OnTriggerEnter2D(collision);
            return;
        }

        base.OnTriggerStay2D(collision);
    }

}
