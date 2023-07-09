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
public class LegacyItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }

    public List<Spawnable> Items = new List<Spawnable>();

    float _totalWeight;

    private void Awake()
    {
        _totalWeight = 0;

        foreach (Spawnable spawnable in Items)
        {
            _totalWeight += spawnable.weight;
        }
    }

    private void Start()
    {
        float _pick = Random.value * _totalWeight;
        int _chosenIndex = 0;
        float _cumulativeWeight = Items[0].weight;

        while (_pick > _cumulativeWeight && _chosenIndex < Items.Count - 1)
        {
            _chosenIndex++;
            _cumulativeWeight += Items[_chosenIndex].weight;
        }

        GameObject ItemSpawned = Instantiate(Items[_chosenIndex].gameObject
            , transform.position
            , Quaternion.identity) as GameObject;
    }
}
