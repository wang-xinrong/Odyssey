using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject[] ProjectilePrefabs = new GameObject[3];

    public Transform UpLaunchPoint;
    public Transform DownLaunchPoint;
    public Transform LeftLaunchPoint;
    public Transform RightLaunchPoint;
    private Transform[] _launchPoints = new Transform[4];
    public GameObject ProjectileManager;

    public PlayerController _playerController;

    public void SetUpLaunchPoints()
    {
        _launchPoints[0] = UpLaunchPoint;
        _launchPoints[1] = DownLaunchPoint;
        _launchPoints[2] = LeftLaunchPoint;
        _launchPoints[3] = RightLaunchPoint;
    }

    public Vector3 GetLaunchPoint(Vector2 directionVector)
    {
        return _launchPoints[Directions.GetDirectionIndex(directionVector)].position;
    }

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        ProjectileManager = GameObject.Find("ProjectileManager");
        SetUpLaunchPoints();
    }

    public void FireProjectile0()
    {
        FireProjectile(ProjectilePrefabs[0], 0);
    }

    public void FireProjectile1()
    {
        FireProjectile(ProjectilePrefabs[1], 1);
    }

    public void FireProjectile2()
    {
        FireProjectile(ProjectilePrefabs[2], 2);
    }

    public void FireProjectile(GameObject projectileToFire, int comboIndex)
    {
        WeaponAttack currAttack = _playerController.weapon.combos[comboIndex];

        GameObject projectile = Instantiate(projectileToFire
            , GetLaunchPoint(GetDirectionFacing())
            , projectileToFire.transform.rotation
            , ProjectileManager.transform);

        projectile.GetComponent<Projectile>().Damage = currAttack.damage;

        projectile.GetComponent<ProjectileDirection>().
            SetDirection(GetDirectionFacing());
    }

    public Vector2 GetDirectionFacing()
    {
        return _playerController.GetDirectionFacing();
    }

    public void ZbjSpecialAttack(GameObject projectileToFire)
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject projectile = Instantiate(projectileToFire
            , GetLaunchPoint(GetDirectionFacing())
            , projectileToFire.transform.rotation
            , ProjectileManager.transform);

            projectile.GetComponent<ProjectileDirection>().
                SetDirection(Directions.EightDirections[i]);
        }
    }
}
