using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    public int startX;
    public int startY;
    public float delayBeforeAstarScan;

    // Start is called before the first frame update
    void Start()
    {
        DungeonCrawlerController.GenerateDungeon(dungeonGenerationData, startX, startY);
        // get Astar AI to scan the graph after the rooms have been created
        Invoke("Scan", delayBeforeAstarScan);
    }

    private void Scan()
    {
        GameObject.Find("A*").GetComponent<AstarPath>().Scan();
    }
}
