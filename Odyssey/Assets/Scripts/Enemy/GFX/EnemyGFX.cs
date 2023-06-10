using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    protected AIPath _enemyAIPath;
    protected Animator _animator;
    protected Rigidbody2D _rb;

    // new
    protected Damageable _damageable;
    protected enum State {Idle, Walk, Death, Attack}
    protected State _currentState = State.Idle;

    private string _currAnimation;
    protected enum DirectionFacing { Left, Right }
    protected DirectionFacing _currDirection = DirectionFacing.Right;

    // attack implementation
    public DetectionZone AttackZone;
    [SerializeField]
    protected float _attackDelay = 0.4f;
    protected float _attackTimer = 0f;
    private Transform _targetTransform;
    public Transform TargetTransform
    {
        get
        {
            return _targetTransform;
        }
        set
        {
            _targetTransform = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
        _enemyAIPath = GetComponent<AIPath>();
        _rb = GetComponent<Rigidbody2D>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        DetermineDirectionAndFlipSprite();
        _attackTimer += Time.deltaTime;
        if (_damageable.IsAlive == false)
        {
            _currentState = State.Death;
            PlayAnimation(AnimationNames.VillagerGirlDeath);
        } else if (AttackZone.PlayerDetected && _attackTimer > _attackDelay)
        {
            _currentState = State.Attack;
            // pass the responsibility to the launcher to decide direction
            TargetTransform = AttackZone.TargetTransform;
            PlayAnimation(AnimationNames.VillagerGirlAttack);
            //Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        } else if (_enemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.VillagerGirlWalk);
        } else if (_enemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.VillagerGirlIdle);
        } 
    }
    */

    private void StartIdling()
    {
        _attackTimer = 0f;
        _currentState = State.Idle;
    }

    public void StartAttacking()
    {
        _currentState = State.Attack;
    }

    public void PlayAnimation(string animationName)
    {
        if (_currAnimation != animationName)
        {
            _animator.Play(animationName);
            _currAnimation = animationName;
        }
    }

    protected  void DetermineDirectionAndFlipSprite()
    {
        if (_enemyAIPath.velocity.magnitude > 0.1f)
        {
            if (_enemyAIPath.velocity.x > 0.1f && _currDirection == DirectionFacing.Left)
            {
                _currDirection = DirectionFacing.Right;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_enemyAIPath.velocity.x < -0.1f && _currDirection == DirectionFacing.Right)
            {
                _currDirection = DirectionFacing.Left;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    protected  void FlipSpriteToFacePlayer()
    {
        if (AttackZone.PlayerDetected)
        {
            Vector2 EnemyDirection = Directions.RelativeDirectionVector(gameObject.transform
                , AttackZone.TargetTransform);

            if (EnemyDirection.x > 0)
            {
                _currDirection = DirectionFacing.Right;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (EnemyDirection.x < 0)
            {
                _currDirection = DirectionFacing.Left;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    protected void NonAIDetermineDirectionAndFlipSprite()
    {
        if (_rb.velocity.magnitude > 0.1f)
        {
            if (_rb.velocity.x > 0.1f && _currDirection == DirectionFacing.Left)
            {
                _currDirection = DirectionFacing.Right;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_rb.velocity.x < -0.1f && _currDirection == DirectionFacing.Right)
            {
                _currDirection = DirectionFacing.Left;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }


    public bool IsAlive
    {
        get
        {
            return _currentState != State.Death;
        }
    }
}
