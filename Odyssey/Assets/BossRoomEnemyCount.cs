using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnemyCount : MonoBehaviour
{
    public Room Room;
    public GameObject Boss;
    private int _noOfEnemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        _noOfEnemiesAlive = Boss.GetComponent<BossStageManager>().NoOfBossLives;
    }

    // Update is called once per frame
    void Update()
    {
        Room.NoOfEnemiesAlive = _noOfEnemiesAlive;
    }

    public void OneEnemySummoned()
    {
        _noOfEnemiesAlive++;
    }

    public void OneEnemyKilled()
    {
        _noOfEnemiesAlive--;
    }
}
