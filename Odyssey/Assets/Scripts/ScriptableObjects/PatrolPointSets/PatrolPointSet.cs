using UnityEngine;

public class PatrolPointSet : MonoBehaviour
{
    public string SetName;
    public Transform[] PatrolPoints;

    private void Awake()
    {
        GetComponentInParent<PatrolPointManagerScript>().AddPatrolPointSet(this);
    }
}
