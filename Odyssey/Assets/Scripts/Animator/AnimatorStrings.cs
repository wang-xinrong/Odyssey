using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to store the parameter names
// used in the animator. Parameters from different
// animators, which serve a similar purpose, should
// be named using the same string for ease of
// referrence
public class AnimatorStrings : MonoBehaviour
{
    public static string MoveXInput = "MoveXInput";
    public static string MoveYInput = "MoveYInput";
    public static string CanMove = "CanMove";
    public static string IsWalking = "IsWalking";
    public static string AttackTrigger = "AttackTrigger";
    public static string HasTarget = "HasTarget";
    public static string IsAlive = "IsAlive";
    public static string IsHurt = "IsHurt";
}
