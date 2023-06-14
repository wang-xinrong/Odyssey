using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    public int startX;
    public int startY;
    // Start is called before the first frame update
    void Start()
    {
        DungeonCrawlerController.GenerateDungeon(dungeonGenerationData, startX, startY);
    }

}
