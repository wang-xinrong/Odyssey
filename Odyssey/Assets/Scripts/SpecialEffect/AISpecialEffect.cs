using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AISpecialEffect : MonoBehaviour, EnemyUnderSpecialEffect
{
    private AIPath _aIPath;
    public float _originalMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _aIPath = GetComponent<AIPath>();
        _originalMovementSpeed = _aIPath.maxSpeed;
    }

    public void SlowedDown(float fractionOfOriginalSpeed, float duration)
    {
        _aIPath.maxSpeed = _aIPath.maxSpeed * fractionOfOriginalSpeed;
        Invoke("ResetMovementSpeed", duration);
    }

    private void ResetMovementSpeed()
    {
        // resetting the mnovement speed to the true
        // original movement speed to avoid compounding
        // slowdown effect
        _aIPath.maxSpeed = _originalMovementSpeed;
    }
}
