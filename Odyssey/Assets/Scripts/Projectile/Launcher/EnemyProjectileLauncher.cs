using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileLauncher : MonoBehaviour
{
    public GameObject[] ProjectilePrefabs = new GameObject[2];

    public Transform LaunchPoint;
    public EnemyGFX EnemyGFX;
    private GameObject _projectileManager;
    public Transform[] FiveDirectionLaunchPoints;
    public Transform[] EightDirectionLaunchPoints;

    public Vector3 GetLaunchPoint()
    {
        return LaunchPoint.position;
    }

    private void Awake()
    {
        if (!EnemyGFX)
        {
            Debug.Log("Reference to EnemyGFX is missing");
        }

        _projectileManager = GameObject.Find("ProjectileManager");

        if (!_projectileManager) Debug.Log("No GameObject Named ProjectileManager"
            + "in the Scene");
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
            , _projectileManager.transform);
        // need to settle the direction issues
        projectile.GetComponent<ProjectileDirection>().
            SetDirectionForEnemyProjectile(TargetDirection());
    }

    private Vector2 _targetDirection;
    public Vector2 TargetDirection()
    {
        _targetDirection = Directions.RelativeDirectionVector(LaunchPoint, EnemyGFX.TargetTransform);
        return _targetDirection;
    }




    public void FireProjectileInFiveDirections(GameObject projectileToFire)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject projectile = Instantiate(projectileToFire
            , FiveDirectionLaunchPoints[i].position
            , projectileToFire.transform.rotation
            , _projectileManager.transform);

            if (transform.localScale.x > 0)
            {
                projectile.GetComponent<ProjectileDirection>().
                    SetDirectionForEnemyProjectile(Directions.RightFiveDirections[i]);
            }

            if (transform.localScale.x < 0)
            {
                projectile.GetComponent<ProjectileDirection>().
                    SetDirectionForEnemyProjectile(Directions.LeftFiveDirections[i]);
            }
        }
    }

    public void FireProjectileInEightDirections(GameObject projectileToFire)
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject projectile = Instantiate(projectileToFire
            , EightDirectionLaunchPoints[i].position
            , projectileToFire.transform.rotation
            , _projectileManager.transform);

            projectile.GetComponent<ProjectileDirection>().
                          SetDirectionForEnemyProjectile(Directions.EightDirections[i]);
        }
    }
}
