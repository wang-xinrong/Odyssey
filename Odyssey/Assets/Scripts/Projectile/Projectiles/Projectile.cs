 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileDirection))]
public class Projectile : MonoBehaviour
{
    public enum TargetType { Player, Enemy };
    public TargetType ProjectileTargetType;
    public Directions Direction = new Directions();
    public float Speed = 3f;

    // can abstract out the attack as a component
    // after the knight is updateed with direction class
    //private Attack _attack;
    public int Damage = 10;
    public Vector2 KnockBack = new Vector2(7, 0);

    private Rigidbody2D _rb;

    // direction the projectile would be moving in
    //private Vector2 _directionVector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_attack = GetComponent<Attack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = Speed * Direction.DirectionVector;
    }
}
