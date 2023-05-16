using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]//, typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb;
    private Vector2 _moveInput;
    private Vector2 _directionFacing;

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


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // player can only move if he is not hit
            Animator.SetBool(AnimatorStrings.IsWalking, _moveInput != Vector2.zero);

            // the ChangeAnimationDirection call is temporarily introduced here to
            // fix a bug where if the player is facing left during the
            // attack, yet the right directional key is pressed during
            // the attack, the player would move towards right without
            // its animation facing right after the attack animation.
            // this is because the input system only picks up the movement
            // input once, and the OnMove function would not be called
            // again if the key has been pressed during the attack animation
            ChangeAnimationDirection(_moveInput);

            Rb.velocity = _moveInput * CurrentMoveSpeed;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput != Vector2.zero && CanMove)
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
        }
    }

    public void ChangeAnimationDirection(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero && CanMove)
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, moveInput.y);
            
            _directionFacing.y = moveInput.y;
            _directionFacing.x = moveInput.y == 0 ? moveInput.x : 0;
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
            return Animator.GetBool(AnimatorStrings.CanMove);
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        Rb.velocity = new Vector2(knockBack.x, knockBack.y);
    }

}
