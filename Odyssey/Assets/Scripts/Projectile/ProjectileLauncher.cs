using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private Vector2 _directionFacing;
    public static Vector2[] Directions = new Vector2[4] { Vector2.up
        ,Vector2.down, Vector2.left, Vector2.right};
    // the index of the _directionFacing in the directions array
    private int _directionIndex = 3;

    // an array storing the spawning positions of projectile in
    // up: 0, down: 1, left: 2, right: 3 directions
    public Transform[] LaunchPoints = new Transform[4];
    public PlayerController _playerController;

    public void SetUpDirectionIndex(Vector2 directionFacing)
    {
        if (directionFacing.magnitude != 1)
        {
            Debug.LogError("Invalid direction vector entry");
        }
        if (directionFacing.y == 1) _directionIndex = 0; // up
        if (directionFacing.y == -1) _directionIndex = 1; // down
        if (directionFacing.x == -1) _directionIndex = 2; // left
        if (directionFacing.x == 1) _directionIndex = 3; // right
    }

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        //_directionFacing = _playerController.GetDirectionFacing();
    }

    public void FireProjectile()
    {
        _directionFacing = _playerController.GetDirectionFacing();
        SetUpDirectionIndex(_directionFacing);
        GameObject projectile = Instantiate(ProjectilePrefab
            , LaunchPoints[_directionIndex].position
            // a projectile manager can be placed here to be the parent
            // of the projectiles instantiated
            , ProjectilePrefab.transform.rotation);

        // need to get the direction the player is currently facing and set the direction
        // and the direction fo the projectile here
        projectile.GetComponent<Projectile>().SetDirection(Directions[_directionIndex]);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
