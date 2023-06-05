using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath EnemyAIPath;
    private Animator _animator;

    // new
    private Damageable _damageable;
    private enum State {Idle, Walk, Death, Attack}
    private State _currentState = State.Idle;

    private string _currAnimation;
    private enum DirectionFacing { Left, Right }
    private DirectionFacing _currDirection = DirectionFacing.Right;

    // attack implementation
    public DetectionZone AttackZone;
    [SerializeField]
    private float _attackDelay = 0.4f;
    private float _attackTimer = 0f;
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
    }

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
            Invoke("StartIdling", _animator.GetCurrentAnimatorStateInfo(0).length);
        } else if (EnemyAIPath.velocity.magnitude > 1f)
        {
            _currentState = State.Walk;
            PlayAnimation(AnimationNames.VillagerGirlWalk);
        } else if (EnemyAIPath.velocity.magnitude < 1f || _currentState == State.Idle)
        {
            _currentState = State.Idle;
            PlayAnimation(AnimationNames.VillagerGirlIdle);
        } 
    }

    private void StartIdling()
    {
        _attackTimer = 0f;
        _currentState = State.Idle;
    }

    public void PlayAnimation(string animationName)
    {
        if (_currAnimation != animationName)
        {
            _animator.Play(animationName);
            _currAnimation = animationName;
        }
    }

    private void DetermineDirectionAndFlipSprite()
    {
        if (EnemyAIPath.velocity.magnitude > 0.1f)
        {
            if (EnemyAIPath.velocity.x > 0.1f && _currDirection == DirectionFacing.Left)
            {
                _currDirection = DirectionFacing.Right;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (EnemyAIPath.velocity.x < -0.1f && _currDirection == DirectionFacing.Right)
            {
                _currDirection = DirectionFacing.Left;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
