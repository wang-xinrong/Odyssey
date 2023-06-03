using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller of a Enemy Knight
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    Rigidbody2D Rb;

    private TouchingDirections _touchingDirections;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection = WalkableDirection.Right;

    private Vector2 walkDirectionVector = Vector2.right;

    public DetectionZone AttackZone;

    private Animator _animator;

    public float WalkStopRate = 0.05f;

    private Damageable _damageable;

    [SerializeField]
    public float AttackCooldown
    {
        get
        {
            return _animator.GetFloat(AnimatorStrings.AttackCooldown);
        }
        private set
        {
            _animator.SetFloat(AnimatorStrings.AttackCooldown, Mathf.Max(0, value));
        }
    }

    // a way of controlling the knight movement
    public float WalkSpeed
    {
        get
        {
            if (CanMove)
            {
                return 3f;
            }
            else
            {
                return 0f;
            }
        }
    }


    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1
                    , gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    _walkDirection = WalkableDirection.Right;
                    walkDirectionVector = Vector2.right;
                } else if(value == WalkableDirection.Left)
                {
                    _walkDirection = WalkableDirection.Left;
                    walkDirectionVector = Vector2.left;
                }

            }
        }
    }

    
    private bool _hasTarget = false;

    public bool HasTarget { get
        {
            return _hasTarget;
        } private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimatorStrings.HasTarget, value);
        }
    }




    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimatorStrings.CanMove);
        }
    }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = AttackZone.DetectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        /*
        // used to create a sliding to stop motion
        if (!CanMove)
        {
            Rb.velocity = new Vector2(Mathf.Lerp(WalkSpeed, 0, WalkStopRate) * walkDirectionVector.x
                , Rb.velocity.y);
        } else
        {
        */

        // the knight should not move other than the knockback effect
        // when he is hurt
        if (!_damageable.IsHurt)
        {
            Rb.velocity = new Vector2(WalkSpeed * walkDirectionVector.x
                , Rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else
        {
            Debug.LogError("The current WalkDirection is not set to legal values defined in WalkableDirections");
        }
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        Rb.velocity = new Vector2(knockback.x, knockback.y);
    }

    public Vector2 GetDirections()
    {
        return walkDirectionVector;
    }
}
