using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler 
{
    public DungeonCrawler(Vector2Int startPos)
    {
        position = startPos;
    }

    public Vector2Int position 
    { get; set; }

    public Vector2Int prevPosition
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
            return RoomController.instance.HasPreviousCrawlerBeenTo(position.x + vect.x, position.y + vect.y);
        });

        // if there are no remaining directions for crawler to go, do nothing
        if (options.Count == 0)
        {
            return;
        }
        
        // get a random direction 
        Direction toMove = options[Random.Range(0, options.Count)];
        prev = toMove;

        // add a door to the current room in direction that crawler chooses to move
        RoomController.instance.FindPreviouslyEncounteredRoom(position.x, position.y).addDoor(prev);

        // move crawler in direction chosen
        position += directionMovementMap[toMove];

        // create new RoomInfo, with door opposite to direction of movement:
        // if crawler moved from (0, 0) to (1, 0) by moving right, then
        // create a room at (1, 0) with a door on the left
        RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName()
            , position.x
            , position.y
            , GetOppositeDirection(toMove));
    }
}
