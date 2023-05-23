using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines a detection zone, keeping a list of the
// items currently existing in the zone
public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> DetectedColliders = new List<Collider2D>();
    Collider2D Collider;


    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);
    }
}
