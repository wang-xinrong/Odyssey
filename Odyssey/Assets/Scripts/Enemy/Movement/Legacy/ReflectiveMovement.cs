using UnityEngine;
using System.Collections;

public class ReflectiveMovement : EnemyMovement
{
    private float timer = 0f;
    public float movementResetTime = 3f;
    private Vector3 _lastPosition = Vector3.zero;
    public Transform TopLeftRoomCorner;
    public Transform BottomRightRoomCorner;

    private void Update()
    {
        // temporary fix
        if (!TopLeftRoomCorner || !BottomRightRoomCorner)
        {
            EnemyActivation temp = GetComponent<EnemyActivation>();
            TopLeftRoomCorner = temp.BottomLeftCorner;
            BottomRightRoomCorner = temp.TopRightCorner;
        }

        Move();
        if (timer > movementResetTime && gameObject.transform.position == _lastPosition)
        {
            gameObject.transform.position = Directions.RandomisePosition(TopLeftRoomCorner
                , BottomRightRoomCorner);
            timer = 0f;
        } else
        {
            timer += Time.deltaTime;
            _lastPosition = gameObject.transform.position;
        }
    }



    /*
    public Vector2 MovementDirection;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        // a component for wall collision detection
        //_touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _wallCollider = GetComponent<CapsuleCollider2D>();
        Direction.DirectionVector = MovementDirection;//new Vector2(1, 1);
    }

    
    private void Update()
    {
        Move();
    }
    */
}