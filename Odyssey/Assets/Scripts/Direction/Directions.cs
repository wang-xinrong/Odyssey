using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to handle matters regarding
// directions a given gameObject is facing
public class Directions
{
    public static Vector2[] RightFiveDirections = new Vector2[] { Vector2.up
        , new Vector2(1, 1).normalized
        , Vector2.right
        , new Vector2(1, -1).normalized
        , Vector2.down};

    public static Vector2[] LeftFiveDirections = new Vector2[] { Vector2.up
        , new Vector2(-1, 1).normalized
        , Vector2.left
        , new Vector2(-1, -1).normalized
        , Vector2.down};

    public static Vector2[] EightDirections = new Vector2[] {
        Vector2.left
        , new Vector2(-1, 1).normalized
        , Vector2.up
        , new Vector2(1, 1).normalized
        , Vector2.right
        , new Vector2(1, -1).normalized
        , Vector2.down
        , new Vector2(-1, -1).normalized};

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

    public static Vector2 topLeft = EightDirections[1];
    public static Vector2 topRight = EightDirections[3];
    public static Vector2 bottomRight = EightDirections[5];
    public static Vector2 bottomLeft = EightDirections[7];



    public static void FlipSprite(GameObject obj, Vector2 directionVector)
    {
        if (directionVector.magnitude != 1)
        {
            Debug.LogError("direction vector provided is not a unit vector");
        }
        else
        {
            if (directionVector == topRight)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            if (directionVector == Vector2.up)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (directionVector == topLeft)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            if (directionVector == Vector2.left)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                /*
                Vector3 v = obj.transform.localScale;
                obj.transform.localScale = new Vector3(-1 * v.x, v.y, v.z);
                */
            }
            if (directionVector == bottomLeft)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 225);
            }
            if (directionVector == Vector2.down)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (directionVector == bottomRight)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 315);
            }
            /*
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
                Vector3 v = obj.transform.localScale;
                obj.transform.localScale = new Vector3(-1 * v.x, v.y, v.z);
            }
            
            if (directionVector == Vector2.right)
            {
                obj.transform.localScale = new Vector3(1, 1, 1);
            }
            */
        }
    }

    public static void FlipSpriteWithIndex(GameObject obj, int index)
    {
        obj.transform.rotation = Quaternion.Euler(0, 0, 180 - index * 45);
    }

    // function to rotate objects' sprite with an angle in [0, 360]
    public static void RotateSprite(GameObject obj, float eulerAngle)
    {
        obj.transform.rotation = Quaternion.Euler(0, 0, eulerAngle);
    }



    // function to flip objects' sprite around to the opposite horizontal direction
    public static void FlipSpriteHorizontally(GameObject obj)
    {
        obj.transform.localScale = new Vector3(
            obj.transform.localScale.x * -1
            , obj.transform.localScale.y
            , obj.transform.localScale.z);
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
        playerController.Direction.DirectionVector = v2;
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

    public static Vector3 RandomisePosition(Transform topLeft, Transform bottomRight)
    {
        float xPosition = UnityEngine.Random.Range(bottomRight.position.x, topLeft.position.x);
        float yPosition = UnityEngine.Random.Range(topLeft.position.y, bottomRight.position.y);
        return new Vector3(xPosition, yPosition, 0);
    }

    public static void SetPositionToCentreOfVectorInputs(GameObject gameObject,
        Transform topLeft, Transform bottomRight)
    {
        gameObject.transform.position = new Vector3(
            (topLeft.position.x + bottomRight.position.x) / 2
            , (topLeft.position.y + bottomRight.position.y) / 2
            , (topLeft.position.z + bottomRight.position.z) / 2);
    }

    public static void SetPositionToCentre(GameObject go, Vector3 parentPosition)
    {
        go.transform.position = parentPosition;
    }

    

    // this is used for generation of movement points, thus the point
    // needs not to be removed from the list
    public static Vector3 GetRandomAvaiableMovementPoint(
        List<Vector2> listOfPoints, Vector3 parentPosition)
    {
        int _randomPos = Random.Range(0, listOfPoints.Count - 1);
        return new Vector3(
            listOfPoints[_randomPos].x + parentPosition.x
            , listOfPoints[_randomPos].y + parentPosition.y
            , parentPosition.z);
    }
}
