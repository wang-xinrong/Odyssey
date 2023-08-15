using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSPFirstEnemyDeathSeq : DeathSequence
{
    public GameObject TutorialSPSecondEnemy;
    public override void Initiate()
    {
        TutorialSPSecondEnemy.SetActive(true);
        GetComponent<EnemyDialogueLauncher>().LaunchDialogue();
    }
}
