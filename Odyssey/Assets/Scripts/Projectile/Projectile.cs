using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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

    // function for the projectile launcher to set the direction for the
    // projectile to be move in. The argument provided should be a unit
    // vector
    public void SetDirection(Vector2 directionVector)
    {
        Directions.FlipSprite(gameObject, directionVector);
        Direction.DirectionVector = directionVector;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = Speed * Direction.DirectionVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            damageable.OnHurt(Damage, Direction.ContextualiseDirection(KnockBack));
            Destroy(gameObject);
        }
    }
}
