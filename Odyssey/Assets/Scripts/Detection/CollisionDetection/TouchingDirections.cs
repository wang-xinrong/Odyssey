using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// determines and records wall collision and its direction
public class TouchingDirections : MonoBehaviour
{
    public Rigidbody2D Rb;
    public CapsuleCollider2D TouchingCollider;

    // used to store the results of contacts with the wall
    private RaycastHit2D[] wallContacts = new RaycastHit2D[5];
    public ContactFilter2D CastFilter;
    public float WallCheckDistance = 0.02f;

    [SerializeField]
    private bool _isOnWall;

    // check the DirectionFacing, need to be updated to include the up and down directions later
    public Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0
                                                                           ? Vector2.right
                                                                           : Vector2.left;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
        }
    }

    private void Awake()
    {
        TouchingCollider = GetComponent<CapsuleCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        IsOnWall = TouchingCollider.Cast(wallCheckDirection, CastFilter, wallContacts, WallCheckDistance) > 0; 
    }
}
