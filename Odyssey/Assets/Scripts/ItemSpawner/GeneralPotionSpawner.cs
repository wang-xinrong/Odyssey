using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/GeneralPotionSpawner")]
public class GeneralPotionSpawner : ScriptableObject
{
    public MixedSpawner[] PotionSpawners;
}
