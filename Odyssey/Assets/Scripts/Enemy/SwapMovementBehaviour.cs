using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SwapMovementBehaviour : MonoBehaviour
{
    private Patrol _patrol;
    private AIDestinationSetter _aIDestinationSetter;
    private enum CurrentState { Patrol, Chasing , Idle}
    private CurrentState _currentState = CurrentState.Patrol;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        _patrol = GetComponent<Patrol>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 5f)
        {
            timer += Time.deltaTime;
        } else
        {
            Swap();
            timer = 0f;
        }
    }

    void Swap()
    {
        Debug.Log("swap" + _currentState);
        if (_currentState == CurrentState.Patrol)
        {
            _patrol.enabled = false;
            _aIDestinationSetter.enabled = true;
            _currentState = CurrentState.Chasing;
        }
        else if (_currentState == CurrentState.Chasing)
        {
            Debug.Log(_currentState);
            _aIDestinationSetter.enabled = false;
            _patrol.enabled = true;
            _currentState = CurrentState.Patrol;
        }
    }

    void StopMovement()
    {
        _aIDestinationSetter.enabled = false;
        _patrol.enabled = false;
        _currentState = CurrentState.Idle;
    }
}
