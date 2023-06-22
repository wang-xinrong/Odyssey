using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(EnemyActivation))]
[HelpURL("http://arongranberg.com/astar/documentation/stable/class_wandering_destination_setter.php")]
public class WanderingDestinationSetter : MonoBehaviour
{
    public float radius = 20;
    public float PauseTimeAfterReaching = 1f;

    IAstarAI ai;
    EnemyActivation ea;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        ea = GetComponent<EnemyActivation>();
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
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            Invoke("SetDestinationToRandomAndSearchPath", PauseTimeAfterReaching);
            //ai.destination = PickRandomPoint();
            //ai.SearchPath();
        }
    }

    private void SetDestinationToRandomAndSearchPath()
    {
        ai.destination = PickRandomPoint();
        ai.SearchPath();
    }
}