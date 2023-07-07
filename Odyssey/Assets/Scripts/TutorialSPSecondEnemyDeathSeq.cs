using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSPSecondEnemyDeathSeq : DeathSequence
{
    public override void Initiate()
    {
        GetComponent<EnemyDialogueLauncher>().LaunchDialogue();
    }
}
