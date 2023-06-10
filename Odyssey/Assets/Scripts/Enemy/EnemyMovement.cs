using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller of a Enemy Knight
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D Rb;
    protected Animator _animator;
    protected Collider2D _wallCollider;

    public Directions Direction = new Directions();
    public float WalkSpeed;

    public Vector2 MovementDirection;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        // a component for wall collision detection
        //_touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _wallCollider = GetComponent<CapsuleCollider2D>();
        Direction.DirectionVector = MovementDirection.normalized;//new Vector2(1, 1);
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        /*
        // used to create a sliding to stop motion
        if (!CanMove)
        {
            Rb.velocity = new Vector2(Mathf.Lerp(WalkSpeed, 0, WalkStopRate) * walkDirectionVector.x
                , Rb.velocity.y);
        }
        else
        {
            // the knight should not move other than the knockback effect
            // when he is hurt
        */
        Rb.velocity = new Vector2(WalkSpeed * Direction.DirectionVector.x
                , WalkSpeed * Direction.DirectionVector.y);
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
        Rb.velocity = new Vector2(knockback.x, knockback.y);
    }

    public Vector2 GetDirections()
    {
        return Direction.DirectionVector;
    }

    protected Vector2 OnCollisionEnter2D(Collision2D collision)
    {
        // when colliding with a wall, the movement vector will
        // be reflected by the normal of collision
        Direction.DirectionVector = Vector2.Reflect(Direction.DirectionVector
            , collision.GetContact(0).normal);

        // the sprite will only be flipped vertically if the
        // collision happened to the left or right of the
        // sprite
        if (Math.Abs(collision.GetContact(0).normal.y) < 0.01)
        {
            Directions.FlipSpriteHorizontally(gameObject);
        }

        // return the normal of collision
        return collision.GetContact(0).normal;
    }
}

