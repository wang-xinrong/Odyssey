using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject ItemToSpawn;
    public int MinSpawn;
    public int MaxSpawn;
}
