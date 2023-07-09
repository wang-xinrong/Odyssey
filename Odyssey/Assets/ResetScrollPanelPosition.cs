using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScrollPanelPosition : MonoBehaviour
{
    private Vector3 StartingPosition;
    public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = gameObject.transform.position
            + gameObject.transform.parent.position + Offset;
        OnEnable();
    }

    private void OnEnable()
    {
        gameObject.transform.position = StartingPosition;
    }
}
