using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManagerScript : MonoBehaviour
{
    public Dictionary<string, Transform[]> PatrolPointSets = new Dictionary<string, Transform[]>(0);

    public Transform[] GetPatrolPointSet(string name)
    {
        return PatrolPointSets[name];
    }

    public void AddPatrolPointSet(PatrolPointSet set)
    {
        PatrolPointSets.Add(set.SetName, set.PatrolPoints);
    }

    private void Update()
    {
        Debug.Log(PatrolPointSets.Keys.ToString());
    }
}
