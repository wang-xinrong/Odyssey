using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AISpecialEffect : MonoBehaviour
{
    private AIPath _aIPath;

    // Start is called before the first frame update
    void Start()
    {
        _aIPath = GetComponent<AIPath>();
    }
}
