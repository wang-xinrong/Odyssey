using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Damageable _damageable;
    public Directions Direction = new Directions();

    // the player should only be able to call move
    // related functions if he is in the state of
    // idle or walk
    public enum State { Idle, Walk, Death, Attack, Hurt }
    public State _currentState = State.Idle;
    private string _currAnimation;

    public Weapon weapon;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;

    public InputActionProperty m_MovementInput;

    public float CurrentMoveSpeed
    {
        get
        {
            return 5f;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        OnMove(m_MovementInput);

        // if hurt, movement update by input should be disabled
        // and only the knockback from the hit should be moving
        // the character
        if (_currentState == State.Walk)
        {
            PlayAnimation(AnimationNames.ZbjWalk);
            // player can only move if he is not hit
            //Animator.SetBool(AnimatorStrings.IsWalking, _isWalking);
            _rb.velocity = _moveInput * CurrentMoveSpeed;
        }

        if (_currentState == State.Idle)
        {
            PlayAnimation(AnimationNames.ZbjIdle);
            _rb.velocity = Vector2.zero;
        }

        if (_currentState == State.Attack)
        {
            _rb.velocity = Vector2.zero;
        }
    }


    /*
    private void FixedUpdate()
    {
        OnMove(m_MovementInput);

        // if hurt, movement update by input should be disabled
        // and only the knockback from the hit should be moving
        // the character
        if (_currentState == State.Walk)
        {
            PlayAnimation(AnimationNames.ZbjWalk);
            // player can only move if he is not hit
            //Animator.SetBool(AnimatorStrings.IsWalking, _isWalking);
            _rb.velocity = _moveInput * CurrentMoveSpeed;
        }

        if (_currentState == State.Idle)
        {
            PlayAnimation(AnimationNames.ZbjIdle);
            _rb.velocity = Vector2.zero;
        }

        if (_currentState == State.Attack)
        {
            _rb.velocity = Vector2.zero;
        }
    }
    */

    public void PlayAnimation(string animationName)
    {
        if (_currAnimation != animationName)
        {
            Animator.Play(animationName);
            _currAnimation = animationName;
        }
    }

    public void OnMove(InputActionProperty input)// InputAction.CallBackContext)
    {
        // always pickup the input first
        _moveInput = input.action.ReadValue<Vector2>();
        //_moveInput = context.ReadValue<Vector2>();
        Debug.Log(_moveInput);
        Debug.Log(_currentState);

        // the player walk or idle animation should only be updated if he is
        // in the state of idle or walk
        if (_moveInput != Vector2.zero &&
            (_currentState == State.Idle || _currentState == State.Walk))
        {
            _currentState = State.Walk;
            Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            //PlayAnimation(AnimationNames.ZbjWalk);
            Direction.DirectionVector = Directions.StandardiseDirection(_moveInput);
            rotatePoleAttackHitBox();
        }

        if (_moveInput == Vector2.zero &&
            (_currentState == State.Idle || _currentState == State.Walk))
        {
            _currentState = State.Idle;
            //PlayAnimation(AnimationNames.ZbjIdle);
        }
    }

    private void rotatePoleAttackHitBox() 
    {
        Transform PoleAttack = this.gameObject.transform.Find("PoleAttack");
        if (PoleAttack == null) 
        {
            return;
        }
        if (Direction.DirectionVector == Vector2.up)
        {
            PoleAttack.rotation = Quaternion.Euler(0, 0, 180);
        } else if (Direction.DirectionVector == Vector2.left)
        {
            PoleAttack.rotation = Quaternion.Euler(0, 0, -90);
        } else if (Direction.DirectionVector == Vector2.right)
        {
            PoleAttack.rotation = Quaternion.Euler(0, 0, 90);
        } else 
        {
            PoleAttack.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Attack();
        }
    }

    [SerializeField]
    private float _attackDelay = 1.5f;
    [SerializeField]
    private float _inComboBetweenAttackDelay = 0.5f;
    [SerializeField]
    private float _timeBeforeComboCountCleared = 2f;


    private void Attack()
    {
        // check if sufficient time has passed since previous combo was executed
        // & that current combo counter is within bounds
        if (Time.time - lastComboEnd > _attackDelay && comboCounter <= weapon.comboCount)
        {
            // check if sufficient time has passed since previous click to register next attack
            if (Time.time - lastClickedTime > _inComboBetweenAttackDelay)
            {
                lastClickedTime = Time.time;
                _currentState = State.Attack;
                // cancel invocation of all method calls with the indicated names
                // in this behaviour
                CancelInvoke("StartIdling");
                CancelInvoke("InterruptCombo");
                Debug.Log("Invoke called here");

                Attack script = this.gameObject.transform.GetChild(0).GetComponent<Attack>();
                if (script)
                {
                    script.AttackDamage = weapon.combos[comboCounter].damage;
                }
                PlayAnimation(weapon.combos[comboCounter].animationName);

                Debug.Log("current animation played is " + _currAnimation);

                comboCounter++;


                if (comboCounter > weapon.comboCount - 1)
                {
                    FinishCombo();
                    //Invoke("StartIdling", Animator.GetCurrentAnimatorStateInfo(0).length);
                }

                Debug.Log("length of the animation is " + Animator.GetCurrentAnimatorStateInfo(0).length);
                //Invoke("StartIdling", 1);//Animator.GetCurrentAnimatorStateInfo(0).length);
                Invoke("InterruptCombo", _timeBeforeComboCountCleared
                    + Animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    private void StartIdling()
    {
        Debug.Log("Idle is called");
        _currentState = State.Idle;
        //PlayAnimation(AnimationNames.ZbjIdle);

    }

    private void FinishCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        //_currentState = State.Idle;
    }

    private void InterruptCombo()
    {
        comboCounter = 0;
    }

    private void DebugLog()
    {
        Debug.Log("12145123413451345462324161332451545");
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        if (_damageable.IsAlive)
        {
            _rb.velocity = new Vector2(knockback.x, knockback.y);
        }
        else
        {
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
}
