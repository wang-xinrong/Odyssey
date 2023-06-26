using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable), typeof(Animator))]

// this version of PlayerController script is the result of
// an attempt to transform the animator into one that is solely
// controller by the animator and uses no transition links

// and this copy of PlayerController is meant to be used with
// the DebuggingCopyAnimator in which the transition links
// have been removed

// however, there are certain bugs in this script yet to be resolved
// one of them is the incapability of the character to enter attack
// state from idle state.
public class PlayerController : MonoBehaviour, PlayerUnderSpecialEffect
{
    public Animator Animator;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Damageable _damageable;
    public Directions Direction = new Directions();

    // the player should only be able to call move
    // related functions if he is in the state of
    // idle or walk
    public enum State { Idle, Walk, Death, Attack, Hurt, Special }
    public State _currentState = State.Idle;
    private string _currAnimation;
    public float MovementSpeed = 5f;

    public Weapon weapon;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;

    public MainPlayerController _mainPlayerController;
    public SpecialAttack _specialAttack;
    public int charNumber;

    public InputActionProperty m_MovementInput;

    // new, for weapon pickup
    public string charName;
    public bool canPickUp { get; set; }
    public WeaponPickup weaponOnFloor;

    private float _originalMovementSpeed;

    public float CurrentMoveSpeed
    {
        get
        {
            return MovementSpeed;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
        _specialAttack = GetComponent<SpecialAttack>();
        //weapon = GetComponent<Weapon>();
        // the default direction setup for the sprite
        Direction.DirectionVector = Vector2.down;

        // once set the original movement speed should not be changed
        // to keep a record of the original movement speed in preparation
        // for the need to reset movement speed
        _originalMovementSpeed = MovementSpeed;
    }


    private void Update()
    {
        //Debug.Log(PauseMenu.instance);
        if (PauseMenu.instance.isPaused)
        {
            return;
        }
        OnMove(m_MovementInput);

        // if hurt, movement update by input should be disabled
        // and only the knockback from the hit should be moving
        // the character
        if (_currentState == State.Walk)
        {
            // player can only move if he is not hit
            _rb.velocity = _moveInput * CurrentMoveSpeed;
        }

        if (_currentState == State.Idle)
        {
            _rb.velocity = Vector2.zero;
        }

        if (_currentState == State.Attack || _currentState == State.Special)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (_currAnimation != animationName)
        {
            Animator.Play(animationName);
            _currAnimation = animationName;
        }
    }

    public void OnMove(InputActionProperty input)
    {
        // always pickup the input first
        _moveInput = input.action.ReadValue<Vector2>();

        // the player walk or idle animation should only be updated if he is
        // in the state of idle or walk
        if (_moveInput != Vector2.zero &&
            (_currentState == State.Idle || _currentState == State.Walk))
        {
            _currentState = State.Walk;
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            PlayAnimation(weapon.CharWalk);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        }

        if (_moveInput == Vector2.zero &&
            (_currentState == State.Idle || _currentState == State.Walk))
        {
            _currentState = State.Idle;
            PlayAnimation(weapon.CharIdle);
        }

        // this if-else branch of code allows the direction of the character to be updated
        // even when he is in the attack state
        if (_moveInput != Vector2.zero &&
            (_currentState == State.Attack))
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_currentState == State.Death) return;

        if (context.started)
        {
            Attack();
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (!_mainPlayerController.hasSufficientSP(_specialAttack.specialAttackCost))
        {
            return;
        }
        if (!_mainPlayerController.specialAttackOffCD(charNumber))
        {
            Debug.Log("on cd");
            return;
        }
        CancelInvoke("StartIdling");
        _currentState = State.Special;
        PlayAnimation(weapon.CharSpecial);
        _mainPlayerController.decrementSPBy(_specialAttack.specialAttackCost, charNumber);
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (!canPickUp)
        {
            return;
        }
        Weapon tmp = weaponOnFloor.droppedWeapon;
        weaponOnFloor.UpdateSprite(weapon);
        weapon = tmp;
        _mainPlayerController.displaySwappedWeapon(weapon);
    }


    [SerializeField]
    private float _attackDelay = 1.5f;
    [SerializeField]
    private float _inComboBetweenAttackDelay = 0.5f;
    [SerializeField]
    private float _timeBeforeComboCountCleared = 2f;


    // helper method that takes a reference time and checks if the interval between the current
    // and reference time exceeds the given interval duration
    private bool HasSufficientTimePassed(float referenceTime, float intervalDuration)
    {
        return Time.time - referenceTime > intervalDuration; 
    }

    private void Attack()
    {
        // check if sufficient time has passed since previous combo was executed
        // & that current combo counter is within bounds
        if (!HasSufficientTimePassed(lastComboEnd, _attackDelay) || comboCounter > weapon.comboCount)
        {
            return;
        }

        // check if sufficient time has passed since previous click to register next attack
        if (!HasSufficientTimePassed(lastClickedTime, _inComboBetweenAttackDelay))
        {
            return;
        }
        lastClickedTime = Time.time;
        _currentState = State.Attack;
        //_currentState = State.Attack;
        // cancel invocation of all method calls with the indicated names
        // in this behaviour
        CancelInvoke("StartIdling");
        CancelInvoke("InterruptCombo");

        PlayAnimation(weapon.combos[comboCounter].animationName);

        comboCounter++;

        if (comboCounter > weapon.comboCount - 1)
        {
            FinishCombo();
        }

        Invoke("InterruptCombo", _timeBeforeComboCountCleared
            + Animator.GetCurrentAnimatorStateInfo(0).length);
    }

    // called by animation events
    private void StartIdling()
    {
        _currentState = State.Idle;
    }

    private void FinishCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    private void InterruptCombo()
    {
        comboCounter = 0;
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        if (_damageable.IsAlive)
        {
            _currentState = State.Hurt;
            PlayAnimation(weapon.CharHurt);
            _rb.velocity = new Vector2(knockback.x, knockback.y);
        }
        else
        {
            _currentState = State.Death;
            PlayAnimation(weapon.CharDeath);
            // the player would not be able to move the character
            // if it is dead after taking the damage
            _rb.velocity = Vector2.zero;
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

    // implementation of methods in UnderSpecialEffect
    public void Bewitched(float duration)
    {
        ReverseMovementSpeed();
        _mainPlayerController.CanSwap = false;
        Invoke("ResetMovementSpeed", duration);
        Invoke("SetCanSwapTrue", duration);
    }

    private void ReverseMovementSpeed()
    {
        // applying a absolute value function over the movement speed
        // makes sure that the charm effect does not dwell upon each
        // other and lead to confusing cases
        MovementSpeed = -1 * Mathf.Abs(MovementSpeed);
    }

    // to prevent exponential dwelling of slowdown effects,
    // the _originalMovementSpeed variable should only be updated once.

    // the current implementation of slowDown function would then allow
    // adding up of slowdown effect durations. since once a slowdown effect
    // is inflicted upon the enemy, the effect counter will be renewed, as
    // intn

    public void SlowedDown(float fractionOfOriginalSpeed, float duration)
    {
        MovementSpeed = _originalMovementSpeed * fractionOfOriginalSpeed;
        Invoke("ResetMovementSpeed", duration);
    }
    
    private void ResetMovementSpeed()
    {
        MovementSpeed = _originalMovementSpeed;
    }

    private void SetCanSwapTrue()
    {
        _mainPlayerController.CanSwap = true;
    }
}
