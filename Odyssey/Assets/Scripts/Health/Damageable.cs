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
    [SerializeField]
    private bool _isInvincible = false;

    public bool IsHurt { get
        {
            return _animator.GetBool(AnimatorStrings.IsHurt);
        }
        private set
        {
            _animator.SetBool(AnimatorStrings.IsHurt, value);
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

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    private float _timeSinceHit = 0;
    public float InvincibleTime = 2.5f;

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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Hurt(int damage, Vector2 knockback)
    {
        if (IsAlive && !_isInvincible)
        {
            Health -= damage;
            _isInvincible = true;
            IsHurt = true;
            // any function that is subscribed to this Unity event
            // is going to invoke the method with damage and knockback
            // as parameters. The question mark here makes sure only
            // a non-null object will be calling the method
            DamageableHit?.Invoke(damage, knockback);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInvincible)
        {
            if (_timeSinceHit > InvincibleTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }

            _timeSinceHit += Time.deltaTime;
        }
    }
}
