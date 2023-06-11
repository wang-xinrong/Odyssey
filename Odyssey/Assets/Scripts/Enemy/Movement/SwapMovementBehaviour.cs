using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SwapMovementBehaviour : MonoBehaviour
{
    private Patrol _patrol;
    private AIDestinationSetter _aIDestinationSetter;
    public enum CurrentState { Patrol, Chasing , Idle}
    public CurrentState _currentState = CurrentState.Patrol;
    public Collider2D DetectionZoneCollider;
    private DetectionZone _detectionZone;


    // Start is called before the first frame update
    void Start()
    {
        _patrol = GetComponent<Patrol>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _detectionZone = DetectionZoneCollider.GetComponent<DetectionZone>();

        // the default state should be patrolling
        Swap(CurrentState.Patrol);
    }

    // Update is called once per frame
    void Update()
    {
        // chases the player if he is detected, else resume patrolling
        if (_detectionZone.PlayerDetected) Swap(CurrentState.Chasing);
        if (!_detectionZone.PlayerDetected) Swap(CurrentState.Patrol);
    }

    void Swap(CurrentState state)
    {
        // no need to swap if the state to swapped into
        // is the current state
        if (state == _currentState) return;

        if (state == CurrentState.Chasing)
        {
            _patrol.enabled = false;
            _aIDestinationSetter.enabled = true;
            _currentState = CurrentState.Chasing;
        }
        else if (state == CurrentState.Patrol)
        {
            _aIDestinationSetter.enabled = false;
            _patrol.enabled = true;
            _currentState = CurrentState.Patrol;
        }
        else if (state == CurrentState.Idle)
        {
            _aIDestinationSetter.enabled = false;
            _patrol.enabled = false;
            _currentState = CurrentState.Idle;
        }
    }
}
