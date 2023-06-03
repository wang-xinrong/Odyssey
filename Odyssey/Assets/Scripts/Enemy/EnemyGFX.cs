using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath EnemyAIPath;
    private bool _isWalking;
    private bool _isIdling;
    private Animator _animator;
    private string _currAnimation;
    private enum DirectionFacing { Left, Right }
    private DirectionFacing _currDirection = DirectionFacing.Right;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineDirectionAndFlipSprite();

        if (EnemyAIPath.velocity.magnitude < 1f)
        {
            _isIdling = true;
            _isWalking = false;
            PlayAnimation(AnimationNames.VillagerGirlIdle);
        } else if (EnemyAIPath.velocity.magnitude > 1f)
        {
            _isIdling = false;
            _isWalking = true;
            PlayAnimation(AnimationNames.VillagerGirlWalk);
        }
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
