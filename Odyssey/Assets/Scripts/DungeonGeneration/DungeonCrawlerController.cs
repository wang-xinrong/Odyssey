using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up, left, down, right, unset
};
public class DungeonCrawlerController : MonoBehaviour
{
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    public static void GenerateDungeon(DungeonGenerationData dungeonData, int startX, int startY)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        // adds number of dungeon crawlers according to amount specified in dungeonData
        for (int i = 0; i < dungeonData.numCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(new Vector2Int(startX, startY)));
        }

        // Create the Start room that all crawlers will start from
        RoomController.instance.LoadRoom("Start", startX, startY, Direction.unset);

        // depth first crawling
        foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
        {
            // get random steps
            int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);
            for (int i = 0; i < iterations; i++)
            {
                dungeonCrawler.Move(directionMovementMap);
            }
            RoomController.instance.endRoomPositions.Add(dungeonCrawler.position);
        }
    }
}
