using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb;
    private Vector2 _moveInput;
    private Damageable _damageable;
    public Directions Direction = new Directions();
    private bool _isWalking = false;
    private bool _isIdling = true;
    private bool _canMove = true;


    public float CurrentMoveSpeed {  get
        {
            if (_canMove)
            {
                return 5f;
            } else
            {
                // movement locked
                return 0f;
            }
        }
    }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        // if hurt, movement update by input should be disabled
        // and only the knockback from the hit should be moving
        // the character
        if (!_damageable.IsHurt)
        {
            if (_isWalking)
            {
                // player can only move if he is not hit
                Animator.SetBool(AnimatorStrings.IsWalking, _isWalking);
                Rb.velocity = _moveInput * CurrentMoveSpeed;
            }
            else
            {
                Rb.velocity = Vector2.zero;
            }
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        //if (!_damageable.IsAlive) return;
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput != Vector2.zero)// && CanMove)
        {
            _isWalking = true;
            _isIdling = false;
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        } else
        {
            _isWalking = false;
            _isIdling = true;
            Animator.SetBool(AnimatorStrings.IsWalking, !_isIdling);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Animator.SetTrigger(AnimatorStrings.AttackTrigger);
        }
    }
    
    public bool CanMove { get
        {
            return _canMove;
        }
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        if (_damageable.IsAlive)
        {
            Rb.velocity = new Vector2(knockback.x, knockback.y);
        } else
        {
            // the player would not be able to move the character
            // if it is dead after taking the damage
            Rb.velocity = Vector2.zero;
        }
    }

    public Vector2 GetDirectionFacing()
    {
        return Direction.DirectionVector;
    }
}
