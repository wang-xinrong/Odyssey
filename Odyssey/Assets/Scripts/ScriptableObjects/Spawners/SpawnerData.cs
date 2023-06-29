using UnityEngine;

// this spawner is used to generate a number, which
// would be randomly selected from the preset range,
// of the selected gameObject
[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/RangedSpawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject ItemToSpawn;
    public int MinSpawn;
    public int MaxSpawn;
}
