using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDetectionSwapMovement : SwapMovementBehaviour
{
    public Collider2D DetectionZoneCollider;
    private DetectionZone _detectionZone;

    private new void Start()
    {
        base.Start();
        _detectionZone = DetectionZoneCollider.GetComponent<DetectionZone>();
        // the default state should be patrolling
        Invoke("SetBehaviourToSwap", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // chases the player if he is detected, else resume patrolling
        if (_detectionZone.PlayerDetected) Swap(CurrentState.Chasing);
        if (!_detectionZone.PlayerDetected) Swap(CurrentState.Patrol);
    }

    void SetBehaviourToSwap()
    {
        Swap(CurrentState.Patrol);
    }
}
