using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    // Start is called before the first frame update
    void Start()
    {
        DungeonCrawlerController.GenerateDungeon(dungeonGenerationData, 8, 8);
        DungeonCrawlerController.GenerateDungeon(dungeonGenerationData, 0, 0);
    }

}
