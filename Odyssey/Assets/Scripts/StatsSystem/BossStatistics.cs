using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossStatistics : MonoBehaviour
{
    public AIPath AIPath;
    public BossStageManager BossStageManager;
    public Attack BossMeleeAttack;

    // Start is called before the first frame update
    void Start()
    {
        AIPath.maxSpeed = StatsManager.Instance.GetBossMS_AD_SI(
            BossStageManager.BossCodeName, 0);

        BossMeleeAttack.AttackDamage = StatsManager.Instance
            .GetBossMeleeDamage(BossStageManager.BossCodeName);
    }
}
