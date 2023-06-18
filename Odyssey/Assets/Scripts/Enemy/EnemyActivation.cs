using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class EnemyActivation : MonoBehaviour
{
    public Damageable _damageable;

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

    public void SetUpRoomCorners(Transform bottomLeft, Transform topRight)
    {
        BottomLeftCorner = bottomLeft;
        TopRightCorner = topRight;
    }
}
