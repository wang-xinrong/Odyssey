using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : MonoBehaviour
{
    public enum BossStage { One, Two, Three }
    public BossStage _currentBossStage = BossStage.One;
    protected Damageable _damageable;

    // Start is called before the first frame update
    protected void Start()
    {
        _damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
