using UnityEngine;
using System.Collections;

public class NewMonoBehaviour : EnemyMovement
{
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        // a component for wall collision detection
        //_touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _wallCollider = GetComponent<CapsuleCollider2D>();
        Direction.DirectionVector = new Vector2(1, 1);
    }

    private void FixedUpdate()
    {
        //fun();
        ChangeDirections();
        Move();
    }

    private void ChangeDirections()
    {
        Direction.DirectionVector = Direction.ContextualiseDirection(new Vector2(1, 1)).normalized;
        /*
        Direction.DirectionVector = new Vector2(Direction.DirectionVector.x + 0.1f
            , Direction.DirectionVector.y).normalized;
        */
    }
}
