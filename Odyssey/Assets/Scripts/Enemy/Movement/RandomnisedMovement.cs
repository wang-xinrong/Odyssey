using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomnisedMovement : EnemyMovement
{
    private float timer = 0f;
    public float movementResetTime = 3f;
    public Transform TopLeftRoomCorner;
    public Transform BottomRightRoomCorner;

    private void Update()
    {
        Move();

        if (timer > movementResetTime)
        {
            RandomlyChangeDirection();
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void RandomlyChangeDirection()
    {
        Vector2 targetPosition = Directions
            .RandomisePosition(TopLeftRoomCorner, BottomRightRoomCorner);

        Direction.DirectionVector = new Vector2(targetPosition.x - transform.position.x
            , targetPosition.y - transform.position.y).normalized;
    }
}
