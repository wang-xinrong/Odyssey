using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    private void Awake()
    {
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


    }
}
