using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileLauncher : MonoBehaviour
{
    public GameObject[] ProjectilePrefabs = new GameObject[2];

    public Transform LaunchPoint;
    public GameObject ProjectileManager;
    private EnemyGFX _enemyGFX;

    public Vector3 GetLaunchPoint()
    {
        return LaunchPoint.position;
    }

    private void Awake()
    {
        _enemyGFX = GetComponent<EnemyGFX>();
    }

    public void FireProjectile0()
    {
        FireProjectile(ProjectilePrefabs[0]);
    }

    public void FireProjectile1()
    {
        FireProjectile(ProjectilePrefabs[1]);
    }

    public void FireProjectile(GameObject projectileToFire)
    {
        Debug.Log("here");
        GameObject projectile = Instantiate(projectileToFire
            , GetLaunchPoint()
            , projectileToFire.transform.rotation
            , ProjectileManager.transform);
        Debug.Log("here1");
        // need to settle the direction issues
        projectile.GetComponent<Projectile>().
            SetDirectionForEnemyProjectile(TargetDirection());
        Debug.Log("here2");
    }

    private Vector2 _targetDirection;
    public Vector2 TargetDirection()
    {
        Debug.Log("here3");
        _targetDirection = Directions.RelativeDirectionVector(LaunchPoint, _enemyGFX.TargetTransform);
        return _targetDirection;
    }
    /*
    {
        get
        {
            return _targetDirection;
        }
        set
        {
            _targetDirection = value;
        }
    }
    */
}
