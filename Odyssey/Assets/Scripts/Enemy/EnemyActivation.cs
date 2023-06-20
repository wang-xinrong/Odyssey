using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class EnemyActivation : MonoBehaviour
{
    public Damageable _damageable;

    private List<Vector2> _availableMovementPoints;
    public Transform BottomLeftCorner;
    public Transform TopRightCorner;


    public void Activate(bool value)
    {
        if (!_damageable.IsAlive) return;

        gameObject.SetActive(value);
    }

    public bool IsAlive()
    {
        return _damageable.IsAlive;
    }

    public void SetUpMovementPoints(List<Vector2> list)
    {
        _availableMovementPoints = list;
    }

    public List<Vector2> AvailableMovementPoints
    {
        get
        {
            return _availableMovementPoints;
        }
    }

    public void SetUpRoomCorners(Transform bottomLeft, Transform topRight)
    {
        BottomLeftCorner = bottomLeft;
        TopRightCorner = topRight;
    }
}
