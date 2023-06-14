using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler 
{
    public Vector2Int position 
    { get; set; }

    public Direction prev
    { get; set; }

    private Direction GetOppositeDirection(Direction dir)
    {
        Direction opp = Direction.unset;
        switch(dir)
        {
            case Direction.up:
                opp = Direction.down;
                break;
            case Direction.down:
                opp = Direction.up;
                break;
            case Direction.left:
                opp = Direction.right;
                break;
            case Direction.right:
                opp = Direction.left;
                break;
        }
        return opp;
    }

    public DungeonCrawler(Vector2Int startPos)
    {
        position = startPos;
    }

    public void Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        List<Direction> options = new List<Direction>(directionMovementMap.Keys);
        // prevent backward navigation: 
        // if crawler previously moved right, then left will now cause the crawler to navigate back to its previous room
        if (prev != Direction.unset)
        {
            options.Remove(GetOppositeDirection(prev));
        }
        // prevent navigating into existing rooms
        options.RemoveAll(direction => {
            Vector2Int vect = directionMovementMap[direction];
            return RoomController.instance.HasPreviousCrawlerBeenTo(vect.x, vect.y);
        });
        // get a random direction 
        Direction toMove = options[Random.Range(0, options.Count)];
        prev = toMove;
        position += directionMovementMap[toMove];
        RoomController.instance.LoadRoom("Start", position.x, position.y);
    }
}
