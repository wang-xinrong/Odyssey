using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// a component that manages the health of the object
// attached to. Sets and decrements its health.
public class Damageable : MonoBehaviour
{
    private Animator _animator;
    public UnityEvent<int, Vector2> DamageableHit;
    public UnityEvent<int, int> HealthUpdated;

    [SerializeField]
    private bool _isInvincible = false;

    private bool _isHurt = false;

    public bool _isAlive = true;

    public bool IsHurt { get
        {
            return _isHurt;

                //_animator.GetBool(AnimatorStrings.IsHurt);
        }
        private set
        {
            _isHurt = value;
            //_animator.SetBool(AnimatorStrings.IsHurt, value);
        }
    }

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            HealthUpdated.Invoke(_health, _maxHealth);

            if (_health <= 0)
            {
                _isAlive = false;
                _animator.SetBool(AnimatorStrings.IsAlive, false);

                // a temporary implementation update the number of enemies existing

                // if in boss room
                if (GetComponentInParent<BossRoomEnemyCount>())
                {
                    GetComponentInParent<BossRoomEnemyCount>()
                        .OneEnemyKilled();
                }
                else if (GetComponent<TutorialEnemy>())
                {
                    //GetComponent<TutorialEnemy>().OnDeath();
                }
                else // if in intermediate room
                {
                    if (GetComponentInParent<Room>()) GetComponentInParent<Room>()
                            .EnemyKilled();
                }
            }
        }
    }

    public void UpdateMaxHPAndHP()
    {
        MaxHealth = StatsManager.Instance.GetCharacterHP(
                GetComponent<PlayerController>()
                .Character);
        Health = MaxHealth;
    }

    [SerializeField]
    // the time after a hit, during which the player would not receive
    // more damage (invincible) but also loses control over its movement
    public float InvincibleTime = 1.5f;

    private float _timeSinceHit = 0;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            _animator.SetBool(AnimatorStrings.IsAlive, value);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (GetComponent<PlayerController>())
        {
            MaxHealth = StatsManager.Instance.GetCharacterHP(
                GetComponent<PlayerController>()
                .Character);
            Health = MaxHealth;
        }
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        //if (_isAlive &&
            if(!_isInvincible)
        {
            _isHurt = true;
            _isInvincible = true;
            Health -= damage;

            // This HurtTrigger is specifically for the knight enemy who is
            // still using the old animator setup. And this line of code
            // should be removed once the knight enemy's animator has been
            // updated
            _animator.SetTrigger(AnimatorStrings.HurtTrigger);

            // any function that is subscribed to this Unity event
            // is going to invoke the method with damage and knockback
            // as parameters.
            DamageableHit.Invoke(damage, knockback);
            // everything subscribing to the CharacterHurt event
            // will be notified with the information of gameObject
            // and damage
            CharacterEvents.CharacterHurt.Invoke(gameObject, damage);
        }
    }

    // return true if the character can be healed,
    // that is he is not dead or at max health
    public bool OnHeal(int healingAmount)
    {
        if (IsAlive)
        {
            int healthLost = MaxHealth - Health;
            if (healthLost > 0)
            {
                // not allowed to heal beyond max health
                Health = Mathf.Min(MaxHealth, Health + healingAmount);

                // report actual heal amount
                CharacterEvents.CharacterHeal(gameObject
                    , Mathf.Min(healingAmount, healthLost));
                return true;
            } else
            {
                // at max health
                return false;
            }
        }
        // dead
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInvincible)
        {
            // the character will return to its
            // vulnerable state only if it is still
            // alive after taking the damage
            if (_timeSinceHit > InvincibleTime && _isAlive)
            {
                _isInvincible = false;
                _isHurt = false;
                _timeSinceHit = 0;
            }

            _timeSinceHit += Time.deltaTime;
        }
    }
}
