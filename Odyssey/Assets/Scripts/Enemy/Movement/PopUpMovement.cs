using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controller of a Enemy Knight
[RequireComponent(typeof(Rigidbody2D))]
public class PopUpMovement : MonoBehaviour
{
    protected Rigidbody2D Rb;
    protected Animator _animator;
    protected Collider2D _wallCollider;

    public Directions Direction = new Directions();

    // x stands for room horizontal length while y
    // stands for room vertical length
    public Transform TopLeftRoomCorner;
    public Transform BottomRightRoomCorner;
    private float _movementTimer = 0f;
    public float PopUpDelay;
    private PopUpShooterGFX _enemyGFX;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        // a component for wall collision detection
        //_touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _wallCollider = GetComponent<CapsuleCollider2D>();
        _enemyGFX = GetComponent<PopUpShooterGFX>();
    }

    private void Update()
    {
        if (!_enemyGFX.IsAlive) return;

        if (_movementTimer > PopUpDelay)
        {
            Move();
            _movementTimer = 0f;
        }
        else
        {
            _movementTimer += Time.deltaTime;
        }
    }

    protected void Move()
    {
        Rb.transform.position = Directions.RandomisePosition(TopLeftRoomCorner, BottomRightRoomCorner);
        _enemyGFX.StartAttacking();
    }

    public void OnHurt(int damage, Vector2 knockback)
    {
    }

    public Vector2 GetDirections()
    {
        return Direction.DirectionVector;
    }



}
