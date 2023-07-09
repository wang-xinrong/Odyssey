using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(EnemyActivation))]
[HelpURL("http://arongranberg.com/astar/documentation/stable/class_wandering_destination_setter.php")]
public class WanderingDestinationSetter : MonoBehaviour
{
    public float radius = 20;
    public float PauseTimeAfterReaching = 1f;
    private Vector3 _previousPosition;
    private Vector3 _currentPosition;
    private float timer = 0f;
    public float StuckCheckTime = 2f;

    IAstarAI ai;
    EnemyActivation ea;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        ea = GetComponent<EnemyActivation>();
        _previousPosition = gameObject.transform.position;
    }

    Vector3 PickRandomPoint()
    {
        return Directions.GetRandomAvaiableMovementPoint(ea.AvailableMovementPoints
            , transform.parent.transform.position);
        // the original implementation that searches
        // for a random point as next destination within
        // a circle centred at the current destination
        // upon reaching it
        /*
        var point = Random.insideUnitSphere * radius;

        //point.y = 0;
        point += ai.position;
        return point;
        */
    }

    void Update()
    {
        _currentPosition = gameObject.transform.position;
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            Invoke("SetDestinationToRandomAndSearchPath", PauseTimeAfterReaching);
        }

        // the code below checks whether the enemy is currently at the same position
        // as StuckCheckTime secs ago. If no, set the current position to be the
        // previous position and perform same check again in StuckCheckTime secs.
        // if the enemy is indeed at the same position, he is assumed to be stuck
        // and would look for an alternative destination. The _previousPosition
        // not be updated in this case since the enemy stayed in the same position
        if (timer < StuckCheckTime)
        {
            timer += Time.deltaTime;
            return;
        }

        // timer >= stuckCheckTime
        timer = 0;

        if (_currentPosition != _previousPosition)
        {
            _previousPosition = _currentPosition;
            return;
        }

        // _currentPosition == _previousPosition
        SetDestinationToRandomAndSearchPath();
    }

    private void SetDestinationToRandomAndSearchPath()
    {
        ai.destination = PickRandomPoint();
        ai.SearchPath();
    }
}