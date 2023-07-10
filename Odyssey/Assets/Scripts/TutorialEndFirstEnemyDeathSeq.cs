using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndFirstEnemyDeathSeq : DeathSequence
{
    public GameObject TutorialEndSecondEnemy;
    public override void Initiate()
    {
        TutorialEndSecondEnemy.SetActive(true);
        GetComponent<EnemyDialogueLauncher>().LaunchDialogue();
    }
}
