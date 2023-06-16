using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData SpawnerData;
    }

    public GridController Grid;
    public RandomSpawner[] SpawnerData;

    public void InitialiseObjectSpawning()
    {
        foreach (RandomSpawner rs in SpawnerData)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {

        int _randomIteration = Random.Range(data.SpawnerData.MinSpawn
            , data.SpawnerData.MaxSpawn + 1);

        for (int i = 0; i < _randomIteration; i ++)
        {
            int _randomPos = Random.Range(0, Grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.SpawnerData.ItemToSpawn
                , Grid.availablePoints[_randomPos]
                , Quaternion.identity
                , transform) as GameObject;

            Grid.availablePoints.RemoveAt(_randomPos);
            Debug.Log("Spawned Object Named " + data.name);
        }
    }
}
