using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public enum Difficulty { Easy, Normal, Hard, Extreme };
    public enum MKLevel { One, Two, Three, Four};
    public enum ZBJLevel { One, Two, Three, Four };
    public Difficulty CurrentDifficulty = Difficulty.Normal;
    public MKLevel CurrentMKLevel = MKLevel.One;
    public ZBJLevel CurrentZBJLevel = ZBJLevel.Two;
    public enum Character { MonkeyKing, Pigsy};

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
            SetUpEnemyStatsArrays();
            SetUpPlayerStatsArrays();
        } else
        {
            Destroy(gameObject);
        }
    }

    // int[]; 0: hp; 1: damage;
    public Dictionary<Difficulty, float[]> DifficultyLevel;
    public Dictionary<Difficulty, float[]> BossDifficultyLevel;
    public Dictionary<string, int[]> HPDamageAndColliderDamage;
    public Dictionary<string, float[]> MovementSpeedAndAttackDelay;
    public Dictionary<Difficulty, float> CoinToHPRatio;

    // MK stats arrays
    public Dictionary<MKLevel, int[]> MKHPAndSA;
    public Dictionary<MKLevel, float[]> MKSPRegenAndMovementSpeed;


    // ZBJ stats arrays
    public Dictionary<ZBJLevel, int[]> ZBJHPAndSA;
    public Dictionary<ZBJLevel, float[]> ZBJSPRegenAndMovementSpeed;

    public Dictionary<MKLevel, int> MKUpgradeCost;
    public Dictionary<ZBJLevel, int> ZBJUpgradeCost;

    //Boss arrays
    public Dictionary<string, int[]> BossHealth;
    public Dictionary<string, float[]> BossMS_AD_SI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpEnemyStatsArrays()
    {
        //HP/MovementSpeed/AttackDelay
        DifficultyLevel = new Dictionary<Difficulty, float[]>
        {
            {Difficulty.Easy, new float[3] { 0.5f, 0.9f, 2 } },
            {Difficulty.Normal, new float[3] { 0.7f, 1, 1.2f } },
            {Difficulty.Hard, new float[3] { 1.2f, 1.2f, 0.8f } },
            {Difficulty.Extreme, new float[3] { 2, 2, 0.5f} },

            /*
             * Play Test 2 Stats
            {Difficulty.Easy, new float[3] { 0.5f, 0.5f, 2 } },
            {Difficulty.Normal, new float[3] { 0.8f, 1, 1 } },
            {Difficulty.Hard, new float[3] { 1.2f, 1.2f, 0.8f } },
            {Difficulty.Extreme, new float[3] { 2, 2, 0.5f} },
            */
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

        //RatioOfMaxAmountOfCoinsDroppedToEnemyHP
        CoinToHPRatio = new Dictionary<Difficulty, float>()
        {
            { Difficulty.Easy, 0.1f },
            { Difficulty.Normal, 0.2f },
            { Difficulty.Hard, 0.3f },
            { Difficulty.Extreme, 0.5f }
        };

        BossHealth = new Dictionary<string, int[]>()
        {
            { "LevelOneBoss", new int[] {600, 600, 600 } }
            //{ "LevelOneBoss", new int[] {1000, 1000, 1000 } }
        };

        BossMS_AD_SI = new Dictionary<string, float[]>()
        {
            {"LevelOneBoss", new float[] { 2, 0.25f, 10 } }
        };
    }

    public int GetHPAndDamage(string nameString, int index)
    {
        return (int) (HPDamageAndColliderDamage[nameString][index]
            * DifficultyLevel[CurrentDifficulty][0]);
    }

    public float GetMovementSpeedAndAttackDelay(string nameString, int index)
    {
        return MovementSpeedAndAttackDelay[nameString][index]
            * DifficultyLevel[CurrentDifficulty][index + 1];
    }

    public float GetCoinToHPRatio()
    {
        return CoinToHPRatio[CurrentDifficulty];
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

    public void SetUpPlayerStatsArrays()
    {
        MKHPAndSA = new Dictionary<MKLevel, int[]>()
        {
            {MKLevel.One, new int[2] {100, 70} },
            {MKLevel.Two, new int[2] {120, 80} },
            {MKLevel.Three, new int[2] {140, 90} },
            {MKLevel.Four, new int[2] {160, 100} }
        };

        MKSPRegenAndMovementSpeed = new Dictionary<MKLevel, float[]>()
        {
            {MKLevel.One, new float[2] {0.8f, 5.0f} },
            {MKLevel.Two, new float[2] { 0.6f, 5.5f} },
            {MKLevel.Three, new float[2] { 0.5f, 6.0f} },
            {MKLevel.Four, new float[2] { 0.4f, 6.5f} }
        };

        ZBJHPAndSA = new Dictionary<ZBJLevel, int[]>()
        {
            {ZBJLevel.One, new int[2] {100, 35} },
            {ZBJLevel.Two, new int[2] {120, 40} },
            {ZBJLevel.Three, new int[2] {140, 45} },
            {ZBJLevel.Four, new int[2] {160, 50} }
        };

        ZBJSPRegenAndMovementSpeed = new Dictionary<ZBJLevel, float[]>()
        {
            {ZBJLevel.One, new float[2] {0.8f, 5.0f} },
            {ZBJLevel.Two, new float[2] { 0.6f, 5.5f} },
            {ZBJLevel.Three, new float[2] { 0.5f, 6.0f} },
            {ZBJLevel.Four, new float[2] { 0.4f, 6.5f} }
        };

        MKUpgradeCost = new Dictionary<MKLevel, int>()
        {
            {MKLevel.One, 600 },
            {MKLevel.Two, 800 },
            {MKLevel.Three, 1000 },
            {MKLevel.Four, 0 }
        };

        ZBJUpgradeCost = new Dictionary<ZBJLevel, int>()
        {
            {ZBJLevel.One, 600 },
            {ZBJLevel.Two, 800 },
            {ZBJLevel.Three, 1000 },
            {ZBJLevel.Four, 0 }
        };
    }

    public float GetCharacterMovementSpeed(Character character)
    {
        if (character == Character.MonkeyKing)
            return MKSPRegenAndMovementSpeed[CurrentMKLevel][1];

        if (character == Character.Pigsy)
            return ZBJSPRegenAndMovementSpeed[CurrentZBJLevel][1];

        return 0;
    }

    public float GetCharacterSPRegenrate(Character character)
    {
        if (character == Character.MonkeyKing)
            return MKSPRegenAndMovementSpeed[CurrentMKLevel][0];

        if (character == Character.Pigsy)
            return ZBJSPRegenAndMovementSpeed[CurrentZBJLevel][0];

        return 0;
    }

    public int GetCharacterHP(Character character)
    {
        if (character == Character.MonkeyKing)
            return MKHPAndSA[CurrentMKLevel][0];

        if (character == Character.Pigsy)
            return ZBJHPAndSA[CurrentZBJLevel][0];

        return 0;
    }

    public int GetCharacterSA(Character character)
    {
        if (character == Character.MonkeyKing)
            return MKHPAndSA[CurrentMKLevel][1];

        if (character == Character.Pigsy)
            return ZBJHPAndSA[CurrentZBJLevel][1];

        return 0;
    }

    public int GetBossHealthByStage(string name, int stage)
    {
        return (int) (BossHealth[name][stage - 1] *
            DifficultyLevel[CurrentDifficulty][0]);
    }

    public float GetBossMS_AD_SI(string name, int index)
    {
        float factor;

        if (index == 0)
        {
            factor = DifficultyLevel[CurrentDifficulty][1];
        }
        else
        {
            factor = DifficultyLevel[CurrentDifficulty][2];
        }

        return BossMS_AD_SI[name][index] * factor;
    }

    public MKLevel NextMKLevel()
    {
        switch (CurrentMKLevel)
        {
            case MKLevel.One:
                return MKLevel.Two;
            case MKLevel.Two:
                return MKLevel.Three;
            case MKLevel.Three:
                return MKLevel.Four;
            default:
                return MKLevel.Four;
        }           
    }

    public ZBJLevel NextZBJLevel()
    {
        switch (CurrentZBJLevel)
        {
            case ZBJLevel.One:
                return ZBJLevel.Two;
            case ZBJLevel.Two:
                return ZBJLevel.Three;
            case ZBJLevel.Three:
                return ZBJLevel.Four;
            default:
                return ZBJLevel.Four;
        }
    }
}
