using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDirection : MonoBehaviour
{
    public Projectile ProjectileScript;

    public void SetDirection(Vector2 directionVector)
    {
        Directions.FlipSprite(gameObject, directionVector);
        ProjectileScript.Direction.DirectionVector = directionVector;
    }

    public void SetDirectionForEnemyProjectile(Vector2 directionVector)
    {
        // some of the projectile sprites might not need to be rotated
        // such as the heart
        /*
        float angle = directionVector.x > 0 ? Mathf.Rad2Deg * Mathf.Atan(directionVector.y / directionVector.x)
            : Mathf.Rad2Deg * Mathf.Atan(directionVector.y / directionVector.x) + 180;
        Directions.RotateSprite(gameObject, angle);
        */
        ProjectileScript.Direction.DirectionVector = directionVector;
    }

    public void SetDirectionForEightDirectionalProjectiles(int index)
    {
        // some of the projectile sprites might not need to be rotated
        // such as the heart
        /*
        float angle = directionVector.x > 0 ? Mathf.Rad2Deg * Mathf.Atan(directionVector.y / directionVector.x)
            : Mathf.Rad2Deg * Mathf.Atan(directionVector.y / directionVector.x) + 180;
        Directions.RotateSprite(gameObject, angle);
        */
        Directions.FlipSpriteWithIndex(gameObject, index);
        ProjectileScript.Direction.DirectionVector = Directions.EightDirections[index];
    }
}
