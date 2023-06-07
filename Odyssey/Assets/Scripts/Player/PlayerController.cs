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
    public Weapon weapon;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;


    public float CurrentMoveSpeed
    {
        get
        {
            if (_canMove)
            {
                return 5f;
            }
            else
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
        if (_moveInput != Vector2.zero && CanMove)
        {
            _isWalking = true;
            _isIdling = false;
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        }
        else
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
            Attack();
        }
        ExitAttack();
    }

    public bool CanMove
    {
        get
        {
            return _canMove;
        }
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        if (_damageable.IsAlive)
        {
            Rb.velocity = new Vector2(knockback.x, knockback.y);
        }
        else
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

    public bool IsAlive()
    {
        return _damageable.IsAlive;
    }

    private void Attack()
    {
        _canMove = false;
        // check if sufficient time has passed since previous combo was executed & that current combo counter is within bounds
        if (Time.time - lastComboEnd > 0.5f && comboCounter <= weapon.comboCount)
        {
            CancelInvoke("allowMovement");
            CancelInvoke("EndCombo");

            // check if sufficient time has passed since previous click to register next attack
            if (Time.time - lastClickedTime > 0.5f)
            {
                Animator.Play(weapon.combos[comboCounter].animationName, 0, 0);
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= weapon.comboCount)
                {
                    comboCounter = 0;
                }
                Invoke("allowMovement", Animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                _canMove = true;
            }
        }
        else
        {
            _canMove = true;
        }
    }

    private void allowMovement()
    {
        _canMove = true;
    }

    private void ExitAttack()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Invoke("EndCombo", 1);
        }
    }

    private void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        _canMove = true;
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
