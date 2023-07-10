using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public enum Difficulty { Easy, Normal, Hard, Extreme };
    public Difficulty CurrentDifficulty = Difficulty.Normal;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
            SetUpStatsArrays();
        } else
        {
            Destroy(gameObject);
        }
    }

    // int[]; 0: hp; 1: damage;
    public Dictionary<Difficulty, float[]> DifficultyLevel;
    public Dictionary<string, int[]> HPDamageAndColliderDamage;
    public Dictionary<string, float[]> MovementSpeedAndAttackDelay;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpStatsArrays()
    {
        DifficultyLevel = new Dictionary<Difficulty, float[]>
        {
            {Difficulty.Easy, new float[3] { 1, 1, 1 } },
            {Difficulty.Normal, new float[3] { 1, 1, 1 } },
            {Difficulty.Hard, new float[3] { 1, 1, 1 } },
            {Difficulty.Extreme, new float[3] { 2, 2, 0.5f} },
        };

        //HP/AttackDamage/BodyCollisionDamage
        HPDamageAndColliderDamage = new Dictionary<string, int[]>()
        {
            { "DiagonalCollider", new int[3] {150, 10, 0 } },
            { "PopUpShooter", new int[3] {100, 10, 0 } },
            { "Collider", new int[3] {450, 0, 10 } },
            { "StationaryShooter", new int[3] {100, 0, 0 } },
            { "Charmer", new int[3] {200, 0, 0 } },
            { "ChasingShooter", new int[3] {400, 0, 5 } },
            { "ChasingMelee", new int[3] {400, 20, 3 } },
        };

        //MovementSpeed/AttackDelay
        MovementSpeedAndAttackDelay = new Dictionary<string, float[]>()
        {
            { "DiagonalCollider", new float[2] {3, 0.4f} },
            { "PopUpShooter", new float[2] {0, 2f } },
            { "Collider", new float[2] {10, 0} },
            { "StationaryShooter", new float[2] {0, 1f} },
            { "Charmer", new float[2] {2, 0.4f} },
            { "ChasingShooter", new float[2] {2, 0.4f} },
            { "ChasingMelee", new float[2] {2, 0.4f} },
        };
    }

    public int GetHPAndDamage(string nameString, int index)
    {
        return HPDamageAndColliderDamage[nameString][index]
            * (int) DifficultyLevel[CurrentDifficulty][0];
    }

    public float GetMovementSpeedAndAttackDelay(string nameString, int index)
    {
        return MovementSpeedAndAttackDelay[nameString][index]
            * DifficultyLevel[CurrentDifficulty][index + 1];
    }

    public void SetDifficultyLevelToEasy()
    {
        CurrentDifficulty = Difficulty.Easy;
    }

    public void SetDifficultyLevelToNormal()
    {
        CurrentDifficulty = Difficulty.Normal;
    }

    public void SetDifficultyLevelToHard()
    {
        CurrentDifficulty = Difficulty.Hard;
    }

    public void SetDifficultyLevelToExtreme()
    {
        CurrentDifficulty = Difficulty.Extreme;
    }
}
