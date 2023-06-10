using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileLauncher : MonoBehaviour
{
    public GameObject[] ProjectilePrefabs = new GameObject[2];

    public Transform LaunchPoint;
    public GameObject ProjectileManager;
    public EnemyGFX EnemyGFX;

    public Vector3 GetLaunchPoint()
    {
        return LaunchPoint.position;
    }

    private void Awake()
    {
        //_enemyGFX = GetComponent<EnemyGFX>();
        if (!EnemyGFX)
        {
            Debug.Log("Reference to EnemyGFX is missing");
        }
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
        GameObject projectile = Instantiate(projectileToFire
            , GetLaunchPoint()
            , projectileToFire.transform.rotation
            , ProjectileManager.transform);
        // need to settle the direction issues
        projectile.GetComponent<Projectile>().
            SetDirectionForEnemyProjectile(TargetDirection());
    }

    private Vector2 _targetDirection;
    public Vector2 TargetDirection()
    {
        _targetDirection = Directions.RelativeDirectionVector(LaunchPoint, EnemyGFX.TargetTransform);
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
