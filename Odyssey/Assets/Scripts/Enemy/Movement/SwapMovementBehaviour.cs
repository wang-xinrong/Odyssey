using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SwapMovementBehaviour : MonoBehaviour
{
    private Patrol _patrol;
    private AIDestinationSetter _aIDestinationSetter;
    public enum CurrentState { Patrol, Chasing , Idle}
    public CurrentState _currentState = CurrentState.Idle;


    // Start is called before the first frame update
    protected void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _aIDestinationSetter.target = GameObject
            .FindGameObjectWithTag("Player").transform;
        ////////////////////////////////////////////////
        // new, these three lines ought to be added into Patrol instead
        PPM = GetComponentInParent<PatrolPointManagerScript>();
        Invoke("temp", 1f);
        ////////////////////////////////////////////////
    }

    PatrolPointManagerScript PPM;
    private void temp()
    {
        Debug.Log(PPM.GetPatrolPointSet("First"));
        _patrol.targets = PPM.GetPatrolPointSet("First");
        _patrol.targets1 = PPM.GetPatrolPointSet("Second");
    }


    // Update is called once per frame

    public void Swap(CurrentState state)
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

    public void ChangePatrolTargetSets(int setIndex)
    {
        _patrol.ChangePatrolTargets(setIndex);
    }
}
