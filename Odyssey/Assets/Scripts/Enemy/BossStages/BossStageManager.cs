using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossStageManager : MonoBehaviour
{
    public enum BossStage { Zero, One, Two, Three }
    public int NoOfBossLives;
    public BossStage _currentBossStage = BossStage.Zero;
    protected Damageable _damageable;
    public int StageOneHealth;
    public int StageTwoHealth;
    public int StageThreeHealth;
    //public static event EnterStage;

    // Start is called before the first frame update
    protected void Start()
    {
        _damageable = GetComponent<Damageable>();
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
        //EnterStage.Invoke(_currentBossStage);
    }

    public void SetBossStageToTwo()
    {
        _damageable.IsAlive = true;
        _damageable.Health = StageTwoHealth;
        _damageable.MaxHealth = StageTwoHealth;
        _currentBossStage = BossStage.Two;
        //EnterStage.Invoke(_currentBossStage);
    }

    public void SetBossStageToThree()
    {
        _damageable.IsAlive = true;
        _damageable.Health = StageThreeHealth;
        _damageable.MaxHealth = StageThreeHealth;
        _currentBossStage = BossStage.Three;
        //EnterStage.Invoke(_currentBossStage);
    }
}
