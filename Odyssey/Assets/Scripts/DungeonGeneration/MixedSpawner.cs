using UnityEngine;
using System.Collections.Generic;

// this spawner is used to generate a number, which
// would be randomly selected from the preset range,
// of the selected gameObject
[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/MixedSpawner")]
public class MixedSpawner : ScriptableObject
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }

    public List<Spawnable> Items = new List<Spawnable>();
}

