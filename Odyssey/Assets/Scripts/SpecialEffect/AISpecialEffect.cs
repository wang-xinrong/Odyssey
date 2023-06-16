using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AISpecialEffect : MonoBehaviour, EnemyUnderSpecialEffect
{
    private AIPath _aIPath;
    private float _originalMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _aIPath = GetComponent<AIPath>();
    }

    public void SlowedDown(float fractionOfOriginalSpeed, float duration)
    {
        _originalMovementSpeed = _aIPath.maxSpeed;
        _aIPath.maxSpeed = _aIPath.maxSpeed * fractionOfOriginalSpeed;
        Invoke("ResetMovementSpeed", duration);
    }

    private void ResetMovementSpeed()
    {
        _aIPath.maxSpeed = _originalMovementSpeed;
    }
}
