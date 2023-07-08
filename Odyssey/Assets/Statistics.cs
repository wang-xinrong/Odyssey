using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Statistics : MonoBehaviour
{
    public string NameString;

    //HP value and invincibility
    public Damageable Damageable;

    //AI movement speed
    public AIPath AIPath;
    public AISpecialEffect AISpecialEffect;

    //Attack Delay
    public EnemyGFX EnemyGFX;

    //collider body and melee attack
    public Attack BodyCollider;
    public Attack AttackCollider;



    // Start is called before the first frame update
    void Awake()
    {
        Damageable = GetComponent<Damageable>();
        AIPath = GetComponent<AIPath>();
        AISpecialEffect = GetComponent<AISpecialEffect>();
    }

    private void Start()
    {
        SetUpHPAndDamage();
        SetUpMovementSpeedAndAttackDelay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpHPAndDamage()
    {
        //HP setup
        Damageable.MaxHealth = StatsManager.Instance
            .GetHPAndDamage(NameString, 0);
        Damageable.Health = Damageable.MaxHealth;

        if (AttackCollider)
        {
            //colliders damage setup
            AttackCollider.AttackDamage = StatsManager.Instance
                .GetHPAndDamage(NameString, 1);
        }

        if (BodyCollider)
        {
            BodyCollider.AttackDamage = StatsManager.Instance
                .GetHPAndDamage(NameString, 2);
        }
    }

    private void SetUpMovementSpeedAndAttackDelay()
    {
        if (AIPath)
        {
            AIPath.maxSpeed = StatsManager.Instance
                .GetMovementSpeedAndAttackDelay(NameString, 0);
            AISpecialEffect._originalMovementSpeed = AIPath.maxSpeed;
        }

        EnemyGFX._attackDelay = StatsManager.Instance
            .GetMovementSpeedAndAttackDelay(NameString, 1);
    }
}
