using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an inefficient way of spawning items since each
// item spawned will require one ItemSpawner. Having
// one centralised spawner that spawns several items
// would be more effcient. This being said, we could
// this script can come in handy later when we are
// implementing dropping of items by enemies where
// the loot has to spawned at the exact location where
// the enemy died.
public class ItemSpawner : MonoBehaviour
{

    /*
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }

    public List<Spawnable> Items = new List<Spawnable>();
    */

    public MixedSpawner MS;

    float _totalWeight;

    float _pick;
    int _chosenIndex;
    float _cumulativeWeight;

    private void Awake()
    {
        _totalWeight = 0;

        foreach (MixedSpawner.Spawnable spawnable in MS.Items)
        {
            _totalWeight += spawnable.weight;
        }
    }

    private void Start()
    {
        _pick = Random.value * _totalWeight;
        _chosenIndex = 0;
        _cumulativeWeight = MS.Items[0].weight;

        while (_pick > _cumulativeWeight && _chosenIndex < MS.Items.Count - 1)
        {
            _chosenIndex++;
            _cumulativeWeight += MS.Items[_chosenIndex].weight;
        }
    }

    public void SpawnObject()
    {
        // to allow the possibility of nothing being spawned
        if (MS.Items[_chosenIndex].gameObject == null) return;

        GameObject ItemSpawned = Instantiate(MS.Items[_chosenIndex].gameObject
            , transform.position
            , Quaternion.identity
            , gameObject.transform.parent) as GameObject;

        // after spawning the object, this script should
        // be deactivated to avoid a second round of spawning
        this.enabled = false;
    }
}
