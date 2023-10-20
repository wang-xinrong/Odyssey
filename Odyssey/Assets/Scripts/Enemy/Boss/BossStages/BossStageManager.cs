using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossStageManager : MonoBehaviour
{
    public string BossCodeName;
    public string[] BossNames;
    public enum BossStage { Zero, One, Two, Three, End }
    public int NoOfBossLives;
    public BossStage _currentBossStage = BossStage.Zero;
    protected Damageable _damageable;
    public int StageOneHealth;
    public int StageTwoHealth;
    public int StageThreeHealth;


    public delegate void EnterBossStage(string bossName, BossStage stage);
    public static event EnterBossStage OnEnterBossStage;

    public delegate void PassBossDamageable(Damageable damageable);
    public static event PassBossDamageable OnPassBossDamageable;

    // Start is called before the first frame update
    protected void Start()
    {
        _damageable = GetComponent<Damageable>();
        SetUpBossHealth();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log("bossStageManager.update");
        if (_damageable.Health >= _damageable.MaxHealth * 2 / 3)
        {
            _currentBossStage = BossStage.One;
        }
        else if (_damageable.Health >= _damageable.MaxHealth * 1 / 3)
        {
            _currentBossStage = BossStage.Two;
        }
        else
        {
            _currentBossStage = BossStage.Three;
        }
        */
    }

    // functions to be called by animation events
    public void SetBossStageToOne()
    {
        _damageable.Health = StageOneHealth;
        _damageable.MaxHealth = StageOneHealth;
        _currentBossStage = BossStage.One;
        OnEnterBossStage.Invoke(BossNames[0], _currentBossStage);
        OnPassBossDamageable.Invoke(_damageable);
    }

    public void SetBossStageToTwo()
    {
        _damageable.IsAlive = true;
        _damageable.Health = StageTwoHealth;
        _damageable.MaxHealth = StageTwoHealth;
        _currentBossStage = BossStage.Two;
        OnEnterBossStage.Invoke(BossNames[1], _currentBossStage);
    }

    public void SetBossStageToThree()
    {
        _damageable.IsAlive = true;
        _damageable.Health = StageThreeHealth;
        _damageable.MaxHealth = StageThreeHealth;
        _currentBossStage = BossStage.Three;
        OnEnterBossStage.Invoke(BossNames[2], _currentBossStage);
    }

    public void SetBossStageToEnd()
    {
        _currentBossStage = BossStage.End;
        OnEnterBossStage.Invoke(null, _currentBossStage);
    }

    public void SetUpBossHealth()
    {
        StageOneHealth = StatsManager.Instance
            .GetBossHealthByStage(BossCodeName, 1);

        StageTwoHealth = StatsManager.Instance
            .GetBossHealthByStage(BossCodeName, 2);

        StageThreeHealth = StatsManager.Instance
            .GetBossHealthByStage(BossCodeName, 3);
    }
}