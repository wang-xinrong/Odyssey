using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines a detection zone, keeping a list of the
// items currently existing in the zone
[RequireComponent(typeof(Collider2D))]
public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> DetectedColliders = new List<Collider2D>();
    Collider2D Collider;
    private Transform _targetTransform;

    public bool PlayerDetected
    {
        get
        {
            return DetectedColliders.Count > 0;
        }
    }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();

        // initalise the _targetTransform to avoid
        // null exceptions
        _targetTransform = gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedColliders.Add(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _targetTransform = collision.gameObject.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);
    }

    public Transform TargetTransform
    {
        get
        {
            return _targetTransform;
        }
    }
}
