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
    public Vector2 _directionFacing;
    private Damageable _damageable;
    public Directions Direction = new Directions();

    public float CurrentMoveSpeed {  get
        {
            if (CanMove)
            {
                return 5f;
            } else
            {
                // movement locked
                return 0f;
            }
        }
    }

    /*
    public bool IsHurt
    {
        get
        {
            return Animator.GetBool(AnimatorStrings.IsHurt);
        }
        private set
        {
            Animator.SetBool(AnimatorStrings.IsHurt, value);
        }
    }
    */

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
            // player can only move if he is not hit
            Animator.SetBool(AnimatorStrings.IsWalking, _moveInput != Vector2.zero);

            /*
            // the ChangeAnimationDirection call is temporarily introduced here to
            // fix a bug where if the player is facing left during the
            // attack, yet the right directional key is pressed during
            // the attack, the player would move towards right without
            // its animation facing right after the attack animation.
            // this is because the input system only picks up the movement
            // input once, and the OnMove function would not be called
            // again if the key has been pressed during the attack animation
            //ChangeAnimationDirection(_moveInput);
            */

            Rb.velocity = _moveInput * CurrentMoveSpeed;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput != Vector2.zero)// && CanMove)
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        }
    }

    /*
    public void ChangeAnimationDirection(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero && CanMove)
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(moveInput);
        }
    }
    */

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Animator.SetTrigger(AnimatorStrings.AttackTrigger);
        }
    }
    
    public bool CanMove { get
        {
            return Animator.GetBool(AnimatorStrings.CanMove);
        }
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        Rb.velocity = new Vector2(knockback.x, knockback.y);
    }

    public Vector2 GetDirectionFacing()
    {
        return Direction.DirectionVector;
    }
}
