using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public int startX;
    public int startY;
    public string[] rooms = new string[]{"Start", "Attack", "SP", "End"};
    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        Vector2Int pos = new Vector2Int(startX, startY);
        for (int i = 0; i < rooms.Length; i++)
        {
            RoomController.instance.LoadRoom(rooms[i], pos.x, pos.y, Direction.right);
            if (i != 0)
            {
                RoomController.instance.FindPreviouslyEncounteredRoom(pos.x, pos.y).addDoor(Direction.left);
            }
            pos += Vector2Int.right;
        }
        // RoomController.instance.LoadRoom("Start", pos.x, pos.y, Direction.unset);
        // pos += Vector2Int.right;
        // RoomController.instance.LoadRoom("Attack", pos.x, pos.y, Direction.unset);
    }
}
