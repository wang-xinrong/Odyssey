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
    public StatsManager.Character Character = StatsManager.Character.MonkeyKing;
    public Animator Animator;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    public Damageable _damageable;
    public Directions Direction = new Directions();
    public MKAttackSpritePatch MKAttackSpritePatch;
    public GhostEffect GhostEffect;

    /*
    private float bewitchedTimer = 0f;
    private float slowDownTimer = 0f;
    private float speedUpTimer = 0f;
    */


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
    public float SpeedBoostLimitFactor = 2;

    // for special effect status
    private bool isBewitched = false;
    private bool isSlowedDown = false;
    private bool isSpedUp = false;
    public UnityEvent<string, int> OnStatusEffect;

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
        //_damageable = GetComponent<Damageable>();
        _specialAttack = GetComponent<SpecialAttack>();
        //weapon = GetComponent<Weapon>();
        // the default direction setup for the sprite
        Direction.DirectionVector = Vector2.down;
    }

    private void Start()
    {
        UpdateMovementSpeed();
    }

    public void UpdateMovementSpeed()
    {
        MovementSpeed = StatsManager.Instance.GetCharacterMovementSpeed(Character);
        _originalMovementSpeed = MovementSpeed;
    }

    private void Update()
    {
        if (GameStatus.Instance.IsGamePaused)
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

            // new, to fix attack sprite bug
            //if (MKAttackSpritePatch) MKAttackSpritePatch.DeactivateSprites();
        }

        if (_currentState == State.Attack || _currentState == State.Special)
        {
            _rb.velocity = Vector2.zero;
        }

        //UpdateEffectTimers();
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
        // commented out to disable the above stated feature
        /*
        if (_moveInput != Vector2.zero &&
            (_currentState == State.Attack))
        {
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
        }
        */
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

    public void StartIdling()
    {
        if (_currentState == State.Attack) TerminateDash();

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
            return;
        }
        // this character is dead
        if (charNumber == 0)
        {
            _mainPlayerController.OnDisplayPrimaryCharacter.Invoke("dead", true);
        }
        else
        {
            _mainPlayerController.OnDisplaySecondaryCharacter.Invoke("dead", true);
        }
        _currentState = State.Death;
        PlayAnimation(weapon.CharDeath);
        // the player would not be able to move the character
        // if it is dead after taking the damage
        _rb.velocity = Vector2.zero;
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
        if (isBewitched) return;

        isBewitched = true;
        //setTimer(bewitchedTimer, duration);
        ReverseMovementSpeed();
        _mainPlayerController.CanSwap = false;
        Invoke("ResetMovementSpeed", duration);
        Invoke("SetCanSwapTrue", duration);
        Invoke("SetIsBewitchedFalse", duration);
        OnStatusEffect.Invoke("bewitched", (int) duration);
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
        if (isSlowedDown) return;

        isSlowedDown = true;
        //setTimer(slowDownTimer, duration);
        MovementSpeed = _originalMovementSpeed * fractionOfOriginalSpeed;
        Invoke("ResetMovementSpeed", duration);
        Invoke("SetIsSlowedDownFalse", duration);
        OnStatusEffect.Invoke("slowed", (int) duration);
    }
    
    private void ResetMovementSpeed()
    {
        MovementSpeed = _originalMovementSpeed;
    }

    private void SetCanSwapTrue()
    {
        _mainPlayerController.CanSwap = true;
    }

    private void SetIsBewitchedFalse()
    {
        isBewitched = false;
    }

    private void SetIsSlowedDownFalse()
    {
        isSlowedDown = false;
    }

    private void SetIsSpedUpFalse()
    {
        isSpedUp = false;
    }

    public bool ReplenishHealth(int amount)
    {
        if (_damageable.Health >= _damageable.MaxHealth) return false;
        // for now just implement the health increase to be instant
        return _damageable.OnHeal(amount);
    }

    public bool SpeedUp(float fractionOfOriginalSpeed, float duration)
    {
        if (isSpedUp) return false;
        if (!_damageable.IsAlive) return false;
        if (CurrentMoveSpeed >= _originalMovementSpeed * SpeedBoostLimitFactor) return false;

        isSpedUp = true;
        MovementSpeed = _originalMovementSpeed * fractionOfOriginalSpeed;
        Invoke("ResetMovementSpeed", duration);
        Invoke("SetIsSpedUpFalse", duration);
        // for now always able to speed up unless dead
        OnStatusEffect.Invoke("haste", (int) duration);
        return true;
    }


    [SerializeField]
    private float dashSpeedMultiplier = 2f;
    [SerializeField]
    private float dashDuration = 0.25f;
    private float priorDashSpeed;
    [SerializeField]
    private float LastDashTime = 0f;
    [SerializeField]
    private float dashCD = 2f;

    private IEnumerator Dash()
    {
        GhostEffect.StartTrail(dashDuration);
        LastDashTime = Time.time;
        priorDashSpeed = MovementSpeed;
        MovementSpeed = priorDashSpeed * dashSpeedMultiplier;
        yield return new WaitForSeconds(dashDuration);

        // check if any speed up / down effect is still around
        TerminateDash();
    }

    private void TerminateDash()
    {
        GhostEffect.TerminateTrail();

        if (isSlowedDown || isSpedUp || isBewitched)
        {
            MovementSpeed = priorDashSpeed;
        }
        else
        {
            ResetMovementSpeed();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (!_mainPlayerController.dashOffCD(LastDashTime, dashCD))
        {
            Debug.Log("dash on cd");
            return;
        }
        StartCoroutine(Dash());
    }








    /*
    private void setTimer(float timer, float value)
    {
        timer = value;
    }


    // functions for status icons

    // this function returns timer count of different effects
    public float GetTimer(string effectName)
    {
        switch (effectName)
        {
            case "Bewitched":
                return bewitchedTimer;

            case "SlowDown":
                return bewitchedTimer;

            case "SpeedUp":
                return bewitchedTimer;

            default:
                return -1;
        }
    }

    // this is called by PlayerScript in every Update() call
    private void UpdateEffectTimers()
    {
        UpdateTimer(bewitchedTimer, "Bewitched");
        UpdateTimer(slowDownTimer, "SlowDown");
        UpdateTimer(speedUpTimer, "SpeedUp");
    }

    // this updates the timer, with the string timerName as
    // the distinguisher between different timers
    private void UpdateTimer(float timer, string timerName)
    {
        if (timer <= 0)
        {
            ActionAfterCountdown(timerName);
            timer = 0;
            return;
        }

        timer -= Time.deltaTime;
    }

    // this function is left blank, for possibly implementation
    // of event invocation or others actions
    private void ActionAfterCountdown(string timerName)
    {
        // can possibly call invoke events here
    }
    */
}
