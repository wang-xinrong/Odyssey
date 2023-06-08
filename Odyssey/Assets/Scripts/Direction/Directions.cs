using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to handle matters regarding
// directions a given gameObject is facing
public class Directions
{
    // use to convert float movement input into
    // direction vectors (the 4 unit direction vector)
    public static Vector2 StandardiseDirection(Vector2 moveInput)
    {
        if (moveInput.y > 0) return Vector2.up;
        if (moveInput.y < 0) return Vector2.down;
        if (moveInput.x > 0) return Vector2.right;
        if (moveInput.x < 0) return Vector2.left;
        return Vector2.zero;
    }

    public static int GetDirectionIndex(Vector2 directionVector)
    {
        if (directionVector == Vector2.up) return 0;
        if (directionVector == Vector2.down) return 1;
        if (directionVector == Vector2.left) return 2;
        if (directionVector == Vector2.right) return 3;
        Debug.LogError("Invalid direction vector entry");
        return -1;
    }

    // function to flip objects' sprite around to 3 other directions
    // except the default right direction
    public static void FlipSprite(GameObject obj, Vector2 directionVector)
    {
        if (directionVector.magnitude != 1)
        {
            Debug.LogError("direction vector provided is not a unit vector");
        }
        else
        {
            // the if-else conditionals set up the direction of the sprite
            if (directionVector == Vector2.up)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (directionVector == Vector2.down)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (directionVector == Vector2.left)
            {
                obj.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (directionVector == Vector2.right)
            {
                obj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    // function to rotate objects' sprite with an angle in [0, 360]
    public static void RotateSprite(GameObject obj, float eulerAngle)
    {
        obj.transform.rotation = Quaternion.Euler(0, 0, eulerAngle);
    }



    // function to flip objects' sprite around to the opposite horizontal direction
    public static void FlipSpriteHorizontally(GameObject obj)
    {
        if (obj.transform.localScale == new Vector3(-1, 1, 1))
        {
            obj.transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            obj.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void FlipMovementDirection()
    {
        DirectionVector = new Vector2(DirectionVector.x * -1, DirectionVector.y * -1);
    }

    public static void SpriteDirectionSetUp(PlayerController playerController
        , Vector2 v2)
    {
        v2 = StandardiseDirection(v2);
        playerController.Animator.SetFloat(AnimatorStrings.MoveXInput, v2.x);
        playerController.Animator.SetFloat(AnimatorStrings.MoveYInput, v2.y);
        //playerController.Direction.DirectionVector = StandardiseDirection(v2);
    }

    private Vector2 _directionVector;

    public Vector2 DirectionVector
    {
        get
        {
            return _directionVector;
        }
        set
        {
            _directionVector = value;
        }
    }

    // transform a given vector value into a value that takes
    // into consideration the direction of the GameObject-in-concern
    // for example, a knockback of (1, 1) have to be adapted the
    // direction of the attack dealer, say he faces left, then the
    // knockback ought to be (1 * -1, 1 * 0) = (-1, 0)
    public Vector2 ContextualiseDirection(Vector2 original)
    {
        return new Vector2(original.x * DirectionVector.x
            , original.y * DirectionVector.y); 
    }

    public static Vector2 RelativeDirectionVector(Transform objTransform, Transform targetTransform)
    {
        return new Vector2(
            targetTransform.position.x - objTransform.position.x
            , targetTransform.position.y - objTransform.position.y);
    }
}
