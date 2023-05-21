using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 3f;

    public int Damage = 10;
    public Vector2 KnockBack = new Vector2(7, 0);

    private Rigidbody2D _rb;

    // direction the projectile would be moving in, default to be right
    private Vector2 DirectionFacing = Vector2.right;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // function for the projectile launcher to set the direction for the
    // projectile to be move in. The argument provided should be a unit
    // vector
    public void SetDirection(Vector2 direction)
    {
        if (direction.magnitude != 1)
        {
            Debug.LogError("direction vector provided is not a unit vector");
        }
        else
        {
            DirectionFacing = direction;
            // the if-else conditionals set up the direction of the sprite
            if (DirectionFacing.y == 1)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (DirectionFacing.y == -1)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (DirectionFacing.x == -1)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = Speed * DirectionFacing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // hit the target, with a knockback in the direction
            // the damage dealer is facing
            damageable.OnHurt(Damage
                           , new Vector2(KnockBack.x * gameObject.transform
                           .localScale.x
                           , KnockBack.y * gameObject
                           .transform.localScale.y));
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
