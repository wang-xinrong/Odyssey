using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack1 : MonoBehaviour
{
    public int AttackDamage = 10;
    public Vector2 KnockBack = Vector2.zero;

    public abstract void OnTriggerEnter2D(Collider2D collision);
}
