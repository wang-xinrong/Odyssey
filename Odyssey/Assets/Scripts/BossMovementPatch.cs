using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static SwapMovementBehaviour;

public class BossMovementPatch : MonoBehaviour
{
    public SwapMovementBehaviour swapMovementBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (swapMovementBehaviour._currentState == CurrentState.Chasing
            && swapMovementBehaviour._aIDestinationSetter.enabled == false)
        {
            swapMovementBehaviour._patrol.enabled = false;
            swapMovementBehaviour._aIDestinationSetter.enabled = true;
        }
        if (swapMovementBehaviour._currentState == CurrentState.Patrol
            && swapMovementBehaviour._patrol.enabled == false)
        {
            swapMovementBehaviour._patrol.enabled = true;
            swapMovementBehaviour._aIDestinationSetter.enabled = false;
        }
    }
}
